using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Users
{
    public class DeleteEvents
    {
        private readonly IEventService _eventService;
        public DeleteEvents(IEventService eventService)
        {
            _eventService = eventService;
        }

        [FunctionName("DeleteEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "events/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest request)
            {
                try
                {
                    await _eventService.DeleteEventAsync(id);
                    return new OkObjectResult(ResponseBuilder.GeneralResponse("EVENT_DELETED"));
                }
                catch (NotFoundException)
                {
                    return new NotFoundObjectResult(ResponseBuilder.ErrorResponse("EVENT_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
