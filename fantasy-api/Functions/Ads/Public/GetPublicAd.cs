using FantasyApi.Data.Base.Requests;
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
    public class GetPublicAd
    {
        private readonly IAdService _adService;
        public GetPublicAd(IAdService adService)
        {
            _adService = adService;
        }

        [FunctionName("GetPublicAd")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "public-promotion")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest filter)
            {
                var data = await _adService.GetAdByRandomRequestAsync();

                if (data == null)
                {
                    return new NotFoundObjectResult(ResponseBuilder.ErrorResponse("NO_ADS_CREATED"));
                }

                return new OkObjectResult(ResponseBuilder.CreationResponse("AD_REQUESTED", data));
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.User);
        }
    }
}
