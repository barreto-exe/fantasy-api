using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
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

        public async Task<IEnumerable<TeamDto>> GetTeamsAsync()
        {
            return await GetItemsAsync<TeamDto>("GetTeams");
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

        public async Task<PaginatedListDto<TeamDto>> GetTeamsPaginatedAsync(TeamsFilter filter)
        {
            var dtos = await GetItemsPaginatedAsync<TeamDto>(filter, "GetTeamsPaginated");

            var teamsByEvent = await GetTeamsByEvent();

            var items = dtos.Items.ToList();
            items.ForEach(team =>
            {
                team.EventIds = from item in teamsByEvent
                                where item.TeamId == team.Id
                                select item.EventId;
            });

            dtos.Items = items;

            if (filter.EventId != null)
            {
                bool hasInvalidEvent = await HasInvalidEvent(filter.EventId.ToString());
                if (hasInvalidEvent)
                {
                    throw new NotFoundException("Event id");
                }

                dtos.Items = dtos.Items.ToList().Where(x => x.EventIds.ToList().Contains((int)filter.EventId));
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

        /// <exception cref="NotFoundException"></exception>
        public async Task<TeamDto> UpdateTeamAsync(TeamUpdateInput input)
        {
            var oldTeam = await GetTeamByIdAsync(input.Id);
            if (oldTeam == null)
            {
                throw new NotFoundException("Team with the requested id");
            }

            bool hasInvalidEvent = await HasInvalidEvent(input.EventIds);
            if (hasInvalidEvent)
            {
                throw new NotFoundException("Some event id");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("idteam", input.Id),
                new MySqlParameter("name_team", input.Name ?? oldTeam.Name),
                new MySqlParameter("event_ids", input.EventIds ?? string.Join(",", oldTeam.EventIds)),
            };

            if (input.Badge != null)
            {
                var blob = await _storageService.UploadAsync(input.Badge);
                parameters.Add(new MySqlParameter("badge_team", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("badge_team", oldTeam.Badge));
            }

            var cmd = _databaseService.GetCommand("UpdateTeam", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetTeamByIdAsync(input.Id);
        }

        private async Task<bool> HasInvalidEvent(string eventIds)
        {
            var inputEvents = eventIds.Split(",");
            var events = await _eventService.GetEventsAsync();
            bool hasInvalidEvent = (from e in events
                                    where inputEvents.Contains(e.Id.ToString())
                                    select e).Count() != inputEvents.Length;
            return hasInvalidEvent || string.IsNullOrEmpty(eventIds);
        }

        /// <exception cref="NotFoundException"></exception>
        public async Task DeleteTeamAsync(int id)
        {
            var item = await GetTeamByIdAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Team with the requested id");
            }

            await DeleteItemAsync("DeleteTeam", "idTeam", id);
        }
    }
}
