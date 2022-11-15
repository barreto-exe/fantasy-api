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
using System.Data;
using FantasyApi.Data;
using FantasyApi.Utils.JWT;
using FantasyApi.Utils.JWT.Enum;

namespace FantasyApi.Functions
{
    public class Function1
    {
        private readonly IBaseDatabaseService _baseDatabaseService;
        public Function1(IBaseDatabaseService baseDatabaseService)
        {
            _baseDatabaseService = baseDatabaseService;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest input)
            {
                return new OkObjectResult("cool");
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
