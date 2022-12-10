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

namespace FantasyApi.Functions.Stickers
{
    public class DeleteStickers
    {
        private readonly IStickerService _stickerService;
        public DeleteStickers(IStickerService stickerService)
        {
            _stickerService = stickerService;
        }

        [FunctionName("DeleteStickers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "stickers/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest request)
            {
                try
                {
                    await _stickerService.DeleteStickerAsync(id);
                    return new OkObjectResult(ResponseBuilder.GeneralResponse("STICKER_DELETED"));
                }
                catch (NotFoundException)
                {
                    return new NotFoundObjectResult(ResponseBuilder.ErrorResponse("STICKER_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
