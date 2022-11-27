using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
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

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            return await GetItemByIdAsync<EventDto>("GetEventById", "eventId", id);
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync()
        {
            return await GetItemsAsync<EventDto>("GetEvents");
        }

        public async Task<PaginatedListDto<EventDto>> GetEventsPaginatedAsync(BaseRequest filter)
        {
            return await GetItemsPaginatedAsync<EventDto>(filter, "GetEventsPaginated");
        }

        /// <exception cref="AlreadyExistsException"></exception>
        public async Task<EventDto> AddEventAsync(EventAddInput input)
        {
            //Validate if email has already been used
            var eventWithName = (await GetEventsAsync())?.FirstOrDefault(x => x.Name == input.EventName);
            if (eventWithName != null)
            {
                throw new AlreadyExistsException("Event with the requested name");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("nameEv", input.EventName),
                new MySqlParameter("activeEv", input.Active),
            };

            if (input.Img != null)
            {
                var blob = await _storageService.UploadAsync(input.Img);
                parameters.Add(new MySqlParameter("imgEv", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("imgEv", ""));
            }

            var cmd = _databaseService.GetCommand("AddEvent", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);
            if (data.Rows.Count > 0)
            {
                int newId = Convert.ToInt32(data.Rows[0][0]);
                return await GetEventByIdAsync(newId);
            }
            else
            {
                return null;
            }
        }

        /// <exception cref="NotFoundException"></exception>
        public async Task<EventDto> UpdateEventAsync(EventUpdateInput input)
        {
            var oldEvent = await GetEventByIdAsync(input.Id);
            if (oldEvent == null)
            {
                throw new NotFoundException("Event with the requested id");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("idEv", input.Id),
                new MySqlParameter("new_name", input.EventName ?? oldEvent.Name),
                new MySqlParameter("new_active", input.Active ?? oldEvent.IsActive),
            };

            if (input.Img != null)
            {
                var blob = await _storageService.UploadAsync(input.Img);
                parameters.Add(new MySqlParameter("new_img", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("new_img", oldEvent.Img));
            }

            var cmd = _databaseService.GetCommand("UpdateEvent", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetEventByIdAsync(input.Id);
        }

        /// <exception cref="NotFoundException"></exception>
        public async Task DeleteEventAsync(int id)
        {
            var item = await GetEventByIdAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Event with the requested id");
            }

            await DeleteItemAsync("DeleteEvent", "idEv", id);
        }
    }
}
