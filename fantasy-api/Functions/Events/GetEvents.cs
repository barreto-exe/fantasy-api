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

namespace FantasyApi.Functions.Events
{
    public class GetEvents
    {
        private readonly IEventService _eventService;
        public GetEvents(IEventService eventService)
        {
            _eventService = eventService;
        }

        [FunctionName("GetEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "events")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest filter)
            {
                var data = await _eventService.GetEventsPaginated(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
