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

namespace FantasyApi.Functions.Ads
{
    public class PutAds
    {
        private readonly IAdService _adService;
        public PutAds(IAdService adService)
        {
            _adService = adService;
        }

        [FunctionName("PutAds")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "promotions/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(AdUpdateInput input)
            {
                try
                {
                    input.Id = id;
                    var result = await _adService.UpdateAdAsync(input);
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("AD_UPDATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("AD_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<AdUpdateInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
