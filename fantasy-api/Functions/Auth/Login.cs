using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FantasyApi.Data;
using FantasyApi.Interfaces;
using FantasyApi.Data.Inputs;

namespace FantasyApi.Functions.Auth
{
    public class Login
    {
        public IAuthService _authService;
        public Login(IAuthService authService)
        {
            _authService = authService;
        }

        [FunctionName("Login")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "login")] HttpRequest req,
            ILogger log)
        {
            return await RequestHandler.Handle<LoginInput>(req, log, async (input) =>
            {
                var data = await _authService.Login(input);

                return new OkObjectResult(data);
            });
        }
    }
}
