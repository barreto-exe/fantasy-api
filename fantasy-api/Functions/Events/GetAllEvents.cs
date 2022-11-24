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
using FantasyApi.Services;
using FantasyApi.Utils.JWT.Enum;

namespace FantasyApi.Functions.Events
{
    public class GetAllEvents
    {
        private readonly IEventService _eventService;
        public GetAllEvents(IEventService eventService)
        {
            _eventService = eventService;
        }

        [FunctionName("GetAllEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "events/all")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest filter)
            {
                var data = await _eventService.GetEventsAsync();
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
