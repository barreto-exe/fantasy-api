using Core.Utils.Mapping;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Teams.Dtos;
using FantasyApi.Data.Teams.Filters;
using FantasyApi.Data.Teams.Inputs;
using FantasyApi.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class TeamsService : BaseService, ITeamsService
    {
        private readonly IEventService _eventService;
        private readonly IAzureStorageService _storageService;

        public TeamsService(IEventService eventService, IAzureStorageService storageService, IDatabaseService databaseService) : base(databaseService)
        {
            _eventService = eventService;
            _storageService = storageService;
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var dto = await GetItemByIdAsync<TeamDto>("GetTeamById", "idTeam", id);

            var sql = $"SELECT id_event FROM teams_by_event WHERE id_team = {id}";
            var cmd = _databaseService.GetCommand(sql, type: CommandType.Text);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var eventIds = from DataRow r in data.Rows
                               select Convert.ToInt32(r[0]);

                dto.EventIds = eventIds;

                return dto;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<TeamDto>> GetTeamsAsync(TeamsFilter filter)
        {
            var dtos = (await GetItemsAsync<TeamDto>("GetTeams")).ToList();

            var teamsByEvent = await GetTeamsByEvent();

            dtos.ForEach(team =>
            {
                team.EventIds = from item in teamsByEvent
                                where item.TeamId == team.Id
                                select item.EventId;
            });

            if (filter.EventId != null)
            {
                bool hasInvalidEvent = await HasInvalidEvent(filter.EventId.ToString());
                if (hasInvalidEvent)
                {
                    throw new NotFoundException("Event id");
                }

                return dtos.Where(x => x.EventIds.ToList().Contains((int)filter.EventId));
            }

            return dtos;
        }

        private async Task<IEnumerable<TeamByEventDto>> GetTeamsByEvent()
        {
            var cmd = _databaseService.GetCommand("GetTeamsByEvent");
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<TeamByEventDto>();
                var item = mapper.Map(data);
                return item;
            }
            else
            {
                return null;
            }
        }

        /// <exception cref="NotFoundException"></exception>
        public async Task<TeamDto> AddTeamAsync(TeamAddInput input)
        {
            bool hasInvalidEvent = await HasInvalidEvent(input.EventIds);
            if (hasInvalidEvent)
            {
                throw new NotFoundException("Some event id");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("team_name", input.Name),
                new MySqlParameter("event_ids", input.EventIds),
            };

            if (input.Badge != null)
            {
                var blob = await _storageService.UploadAsync(input.Badge);
                parameters.Add(new MySqlParameter("team_badge", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("team_badge", ""));
            }

            var cmd = _databaseService.GetCommand("AddTeam", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);
            if (data.Rows.Count > 0)
            {
                int newId = Convert.ToInt32(data.Rows[0][0]);
                return await GetTeamByIdAsync(newId);
            }
            else
            {
                return null;
            }
        }

        private async Task<bool> HasInvalidEvent(string eventIds)
        {
            var inputEvents = eventIds.Split(",");
            var events = await _eventService.GetEventsAsync();
            bool hasInvalidEvent = (from e in events
                                    where inputEvents.Contains(e.Id.ToString())
                                    select e).Count() != inputEvents.Length;
            return hasInvalidEvent;
        }
    }
}
