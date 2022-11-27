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
    public class PostEvents
    {
        private readonly IEventService _eventService;
        public PostEvents(IEventService eventService)
        {
            _eventService = eventService;
        }

        [FunctionName("PostEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "events")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(EventAddInput input)
            {
                try
                {
                    var result = await _eventService.AddEventAsync(input);
                    return new OkObjectResult(ResponseBuilder.CreationResponse("EVENT_CREATED", result));
                }
                catch (AlreadyExistsException)
                {
                    return new BadRequestObjectResult(ResponseBuilder.ErrorResponse("EVENT_ALREADY_EXISTS"));
                }
            }

            return await RequestHandler.Handle<EventAddInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
