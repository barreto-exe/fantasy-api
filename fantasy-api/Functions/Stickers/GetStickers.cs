using FantasyApi.Data.Stickers.Filters;
using FantasyApi.Interfaces;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Stickers
{
    public class GetStickers
    {
        private readonly IStickerService _stickerService;
        public GetStickers(IStickerService stickerService)
        {
            _stickerService = stickerService;
        }

        [FunctionName("GetStickers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stickers")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(StickersFilter filter)
            {
                var data = await _stickerService.GetStickersAsync(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<StickersFilter>(req, log, Action, RoleEnum.Admin);
        }
    }
}
