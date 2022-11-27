using FantasyApi.Data.Base.Requests;
using FantasyApi.Interfaces;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Ads
{
    public class GetAds
    {
        private readonly IAdService _adService;
        public GetAds(IAdService adService)
        {
            _adService = adService;
        }

        [FunctionName("GetAds")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "promotions")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest filter)
            {
                var data = await _adService.GetAdsPaginatedAsync(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
