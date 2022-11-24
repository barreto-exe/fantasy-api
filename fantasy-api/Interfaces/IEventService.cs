using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using FantasyApi.Data.Events.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEventsAsync();

        Task<PaginatedListDto<EventDto>> GetEventsPaginatedAsync(BaseRequest input);

        /// <exception cref="EventExistsException"></exception>
        Task<EventDto> AddEventAsync(EventAddInput input);
    }
}
