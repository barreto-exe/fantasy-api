using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Auth
{
    public class Login
    {
        public readonly IAuthService _authService;
        public Login(IAuthService authService)
        {
            _authService = authService;
        }

        [FunctionName("Login")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(LoginInput input)
            {
                var data = await _authService.Login(input);

                if (data == null)
                {
                    return new BadRequestObjectResult(IAuthService.ErrorCodeBuilder("NO_PASSWORD_MATCH"));
                }

                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<LoginInput>(req, log, Action, RoleEnum.Any);
        }
    }
}
