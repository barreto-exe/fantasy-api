using FantasyApi.Data.Users.Exceptions;
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
    public class PostUsers
    {
        private readonly IUserService _userService;
        public PostUsers(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("PostUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "users")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(UserAddInput input)
            {
                try
                {
                    var result = await _userService.AddUserAsync(input);
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("USER_CREATED", result));
                }
                catch (UserExistsException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("EMAIL_ALREADY_EXISTS"));
                }
            }

            return await RequestHandler.Handle<UserAddInput>(req, log, Action, RoleEnum.Admin);
        }
    }
}
