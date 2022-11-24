using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using FantasyApi.Data.Events.Exceptions;
using FantasyApi.Data.Events.Inputs;
using FantasyApi.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class EventService : BaseService, IEventService
    {
        private readonly IAzureStorageService _storageService;
        public EventService(IDatabaseService databaseService, IAzureStorageService storageService) : base(databaseService)
        {
            _storageService = storageService;
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync()
        {
            var cmd = _databaseService.GetCommand("GetEvents");
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<EventDto>();
                var events = mapper.Map(data);
                return events;
            }
            else
            {
                return null;
            }
        }

        public async Task<PaginatedListDto<EventDto>> GetEventsPaginatedAsync(BaseRequest filter)
        {
            return await GetItemsPaginated<EventDto>(filter, "GetEventsPaginated");
        }

        /// <exception cref="EventExistsException"></exception>
        public async Task<EventDto> AddEventAsync(EventAddInput input)
        {
            //Validate if email has already been used
            var eventWithName = (await GetEventsAsync())?.FirstOrDefault(x => x.Name == input.EventName);
            if (eventWithName != null)
            {
                throw new EventExistsException();
            }

            var blob = await _storageService.UploadAsync(input.Img);

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("nameEv", input.EventName),
                new MySqlParameter("imgEv", blob.Blob.Uri),
                new MySqlParameter("activeEv", input.Active),
            };

            var cmd = _databaseService.GetCommand("AddEvent", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);
            if (data.Rows.Count > 0)
            {
                int newId = Convert.ToInt32(data.Rows[0][0]);
                var dto = (await GetEventsAsync()).FirstOrDefault(x => x.Id == newId);
                return dto;
            }
            else
            {
                return null;
            }
        }
    }
}
