using FantasyApi.Data.Auth.Dtos;
using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Users.Exceptions;
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
    public class Register
    {
        public IAuthService _authService;
        public Register(IAuthService authService)
        {
            _authService = authService;
        }

        [FunctionName("Register")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "register")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(RegisterInput input)
            {
                LoginDto data = null;

                try
                {
                    data = await _authService.Register(input);
                }
                catch(UserExistsException)
                {
                    return new BadRequestObjectResult(IAuthService.ErrorCodeBuilder("EMAIL_ALREADY_EXISTS"));
                }

                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<RegisterInput>(req, log, Action, RoleEnum.Any);
        }
    }
}
