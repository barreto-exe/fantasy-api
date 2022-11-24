using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using FantasyApi.Data.Events.Exceptions;
using FantasyApi.Data.Events.Inputs;
using FantasyApi.Data.Users.Dtos;
using FantasyApi.Data.Users.Exceptions;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using Microsoft.Azure.ServiceBus;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class EventService : BaseService, IEventService
    {
        public EventService(IDatabaseService databaseService) : base(databaseService) { }

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
            var events = await GetEventsAsync();
            bool eventExists = (from u in events
                                where u.Name == input.EventName
                                select u).ToList().Count > 0;
            if (eventExists)
            {
                throw new EventExistsException();
            }

            throw new NotImplementedException();
        }
    }
}
