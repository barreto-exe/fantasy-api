using FantasyApi.Data.Ads.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Ads
{
    public class PostAds
    {
        private readonly IAdService _adService;
        public PostAds(IAdService adService)
        {
            _adService = adService;
        }

        [FunctionName("PostAds")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "promotions")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(AdAddInput input)
            {
                var result = await _adService.AddAdAsync(input);
                return new OkObjectResult(ResponsesBuilder.CreationResponse("AD_CREATED", result));
            }

            return await RequestHandler.Handle<AdAddInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
