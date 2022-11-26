using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Users.Inputs;
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
    public class PutUsers
    {
        private readonly IUserService _userService;
        public PutUsers(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("PutUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "users/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(UserUpdateInput input)
            {
                try
                {
                    input.Id = id;
                    var result = await _userService.UpdateUserAsync(input);
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("USER_UPDATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("USER_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<UserUpdateInput>(req, log, Action, RoleEnum.Admin);
        }
    }
}
