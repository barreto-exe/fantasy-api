using FantasyApi.Data.Base.Exceptions;
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

namespace FantasyApi.Functions.Ads
{
    public class DeleteAds
    {
        private readonly IAdService _adService;
        public DeleteAds(IAdService adService)
        {
            _adService = adService;
        }

        [FunctionName("DeleteAds")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "ads/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest request)
            {
                try
                {
                    await _adService.DeleteAdAsync(id);
                    return new OkObjectResult(ResponsesBuilder.DeletionResponse("AD_DELETED"));
                }
                catch (NotFoundException)
                {
                    return new NotFoundObjectResult(ResponsesBuilder.ErrorResponse("AD_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
