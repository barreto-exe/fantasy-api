using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using FantasyApi.Data.Users.Dtos;
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

        public async Task<IEnumerable<EventDto>> GetEvents()
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

        public async Task<PaginatedListDto<EventDto>> GetEventsPaginated(BaseRequest filter)
        {
            return await GetItemsPaginated<EventDto>(filter, "GetEventsPaginated");
        }
    }
}
