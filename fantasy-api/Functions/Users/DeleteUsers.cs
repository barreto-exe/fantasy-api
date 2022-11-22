using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Users.Exceptions;
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
    public class DeleteUsers
    {
        private readonly IUserService _userService;
        public DeleteUsers(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("DeleteUsers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "users/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest request)
            {
                try
                {
                    await _userService.DeleteUserAsync(id);
                    return new OkObjectResult(ResponsesBuilder.DeletionResponse("USER_DELETED"));
                }
                catch (UserDoesntExistException)
                {
                    return new NotFoundObjectResult(ResponsesBuilder.ErrorResponse("USER_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
