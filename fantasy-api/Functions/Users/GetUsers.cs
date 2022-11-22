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
using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Services;
using FantasyApi.Utils.JWT.Enum;
using FantasyApi.Data;

namespace FantasyApi.Functions.Users
{
    public class GetUsers
    {
        public readonly IUserService _userService;
        public GetUsers(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("GetUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest filter)
            {
                var data = await _userService.GetAllUsersPaginatedAsync(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
