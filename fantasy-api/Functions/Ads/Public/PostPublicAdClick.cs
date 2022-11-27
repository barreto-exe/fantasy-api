using FantasyApi.Data.Ads.Inputs;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Ads.Public
{
    public class PostPublicAdClick
    {
        private readonly IAdService _adService;
        public PostPublicAdClick(IAdService adService)
        {
            _adService = adService;
        }

        [FunctionName("PostPublicAdClick")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "public-promotion/click")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(AdClickInput input)
            {
                try
                {
                    await _adService.AddAdClickAsync(input.Id);
                    return new OkObjectResult(ResponseBuilder.GeneralResponse("AD_CLICKED"));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponseBuilder.ErrorResponse("AD_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<AdClickInput>(req, log, Action, RoleEnum.User);
        }
    }
}
