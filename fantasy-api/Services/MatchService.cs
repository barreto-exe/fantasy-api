using Azure.Core;
using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Matches.Dtos;
using FantasyApi.Data.Matches.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class MatchService : BaseService, IMatchService
    {
        private readonly IAzureStorageService _storageService;
        private readonly ITeamsService _teamsService;
        private readonly IEventService _eventsService;
        private readonly ISoccerPlayerService _playersService;

        public MatchService(IDatabaseService databaseService, IAzureStorageService storageService, ITeamsService teamsService, IEventService eventsService, ISoccerPlayerService playersService) : base(databaseService)
        {
            _storageService = storageService;
            _teamsService = teamsService;
            _eventsService = eventsService;
            _playersService = playersService;
        }

        public async Task<MatchDto> GetMatchByIdAsync(int id)
        {
            MatchDto match = await GetItemByIdAsync<MatchDto>("GetMatchById", "m_id", id);

            match.TeamOne = await _teamsService.GetTeamByIdAsync(match.TeamOneId);
            match.TeamTwo = await _teamsService.GetTeamByIdAsync(match.TeamTwoId);

            match.Players = await GetMatchPlayers(id);

            match.Event = await _eventsService.GetEventByIdAsync(match.EventId);

            return match;
        }

        public async Task<PaginatedListDto<MatchDto>> GetMatchesPaginatedAsync(BaseRequest filter)
        {
            var matches = await GetItemsPaginatedAsync<MatchDto>(filter, "GetMatchesPaginated");

            var teams = await _teamsService.GetTeamsAsync();
            var players = await _playersService.GetPlayersAsync();
            var events = await _eventsService.GetEventsAsync();

            var matchesList = matches.Items.ToList();
            matchesList.ForEach(async m =>
            {
                m.TeamOne = teams.FirstOrDefault(x => x.Id == m.TeamOneId);
                m.TeamTwo = teams.FirstOrDefault(x => x.Id == m.TeamTwoId);

                m.Players = await GetMatchPlayers(m.Id);

                m.Event = await _eventsService.GetEventByIdAsync(m.EventId);
            });

            return matches;
        }

        public async Task<MatchDto> AddMatchAsync(MatchAddInput input)
        {
            bool isValidMatch = await IsValidMatch(input.TeamOneId, input.TeamTwoId, input.EventId);
            if (!isValidMatch)
            {
                throw new Exception("This is not a valid match.");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("m_team1", input.TeamOneId),
                new MySqlParameter("m_team2", input.TeamTwoId),
                new MySqlParameter("m_event_id", input.EventId),
                new MySqlParameter("m_date", input.MatchedAt),
            };

            if (input.MyFile != null)
            {
                var blob = await _storageService.UploadAsync(input.MyFile);
                parameters.Add(new MySqlParameter("m_file_url", blob.Blob.Uri));
            }
            else
            {
                throw new Exception("Is required");
            }

            var cmd = _databaseService.GetCommand("AddMatch", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);
            if (data.Rows.Count > 0)
            {
                int newId = Convert.ToInt32(data.Rows[0][0]);

                bool successFile = await ProcessPerformancesFile(newId, input.MyFile);
                if (!successFile)
                {
                    await DeleteMatchAsync(newId);
                    throw new Exception("This is not a valid file.");
                }

                return await GetMatchByIdAsync(newId);
            }
            else
            {
                return null;
            }
        }

        public async Task<MatchDto> UpdateMatchAsync(MatchUpdateInput input)
        {
            var old = await GetMatchByIdAsync(input.Id);
            if (old == null)
            {
                throw new NotFoundException("Match with the requested id");
            }

            int teamOneId = input.TeamOneId ?? old.TeamOneId;
            int teamTwoId = input.TeamTwoId ?? old.TeamTwoId;
            int eventId = input.EventId ?? old.EventId;
            bool isValidMatch = await IsValidMatch(teamOneId, teamTwoId, eventId);
            if (!isValidMatch)
            {
                throw new Exception("This is not a valid match.");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("m_id", input.Id),
                new MySqlParameter("m_team1", input.TeamOneId ?? old.TeamOneId),
                new MySqlParameter("m_team2", input.TeamTwoId ?? old.TeamTwoId),
                new MySqlParameter("m_event_id", input.EventId ?? old.EventId),
                new MySqlParameter("m_date", input.MatchedAt ?? old.MatchedAt),
            };

            if (input.MyFile != null)
            {
                var blob = await _storageService.UploadAsync(input.MyFile);
                parameters.Add(new MySqlParameter("m_file_url", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("m_file_url", old.MyFile));
            }

            bool successFile = await ProcessPerformancesFile(input.Id, input.MyFile);
            if (!successFile)
            {
                throw new Exception("This is not a valid file.");
            }

            var cmd = _databaseService.GetCommand("UpdateMatch", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetMatchByIdAsync(input.Id);
        }

        public async Task DeleteMatchAsync(int matchId)
        {
            var old = await GetMatchByIdAsync(matchId);
            if (old == null)
            {
                throw new NotFoundException("Match with the requested id");
            }

            await DeleteItemAsync("DeleteMatch", "m_id", matchId);
        }

        private async Task<IEnumerable<PlayerByMatchDto>> GetMatchPlayers(int id)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("m_id", id)
            };

            var cmd = _databaseService.GetCommand("GetMatchPlayers", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<PlayerByMatchDto>();
                var items = mapper.Map(data);
                return items;
            }
            else
            {
                return null;
            }
        }
        private async Task<bool> IsValidMatch(int teamOneId, int teamTwoId, int eventId) 
        {
            var teamOne = await _teamsService.GetTeamByIdAsync(teamOneId);
            var teamTwo = await _teamsService.GetTeamByIdAsync(teamTwoId);

            return teamOne.EventIds.Contains(eventId) && teamTwo.EventIds.Contains(eventId);
        }
        private async Task<bool> ProcessPerformancesFile(int matchId, IFormFile file)
        {
            string csv = null;

            await using(Stream? data = file.OpenReadStream())
            {
                var reader = new StreamReader(data);
                csv = reader.ReadToEnd();
            }

            if (csv == null) return false;

            DataTable dt = csv.CsvToDatatable();

            var tasks = new List<Task>();

            //Delete previous performances if exist
            await DeleteMatchPerformances(matchId);

            foreach (DataRow row in dt.Rows)
            {
                var playerExternalId = row[0].ToString();
                var points = Convert.ToDouble(row[1]);

                tasks.Add(AddPlayerPerformance(matchId, playerExternalId, points));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
        private async Task AddPlayerPerformance(int matchId, string playerExternalId, double points)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("sp_id_match", matchId),
                new MySqlParameter("sp_id_external_player", playerExternalId),
                new MySqlParameter("sp_points", points),
            };

            var cmd = _databaseService.GetCommand("AddMatchPlayerPerformance", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);
        }
        private async Task DeleteMatchPerformances(int matchId)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("m_id", matchId),
            };

            var cmd = _databaseService.GetCommand("DeleteMatchPerformances", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);
        }
    }
}
