using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FantasyApi.Interfaces;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Utils.JWT.Enum;
using FantasyApi.Data.Events.Inputs;
using FantasyApi.Services;
using FantasyApi.Utils;
using FantasyApi.Data.Events.Exceptions;

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
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("EVENT_CREATED", result));
                }
                catch (EventExistsException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("EVENT_ALREADY_EXISTS"));
                }
            }

            return await RequestHandler.Handle<EventAddInput>(req, log, Action, RoleEnum.Admin);
        }
    }
}
