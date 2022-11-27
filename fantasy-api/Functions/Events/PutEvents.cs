using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Events.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Events
{
    public class PutEvents
    {
        private readonly IEventService _eventService;
        public PutEvents(IEventService eventService)
        {
            _eventService = eventService;
        }

        [FunctionName("PutEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "events/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(EventUpdateInput input)
            {
                try
                {
                    input.Id = id;
                    var result = await _eventService.UpdateEventAsync(input);
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("EVENT_UPDATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("EVENT_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<EventUpdateInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
