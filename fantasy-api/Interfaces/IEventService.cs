using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEvents();
        Task<PaginatedListDto<EventDto>> GetEventsPaginated(BaseRequest input);
    }
}
