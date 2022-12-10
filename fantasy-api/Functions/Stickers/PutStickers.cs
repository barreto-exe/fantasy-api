using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Stickers.Inputs;
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
    public class PutStickers
    {
        private readonly IStickerService _stickerService;
        public PutStickers(IStickerService stickerService)
        {
            _stickerService = stickerService;
        }

        [FunctionName("PutStickers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "stickers/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(StickerUpdateInput input)
            {
                try
                {
                    input.Id = id;
                    var result = await _stickerService.UpdateStickerAsync(input);
                    return new OkObjectResult(ResponseBuilder.CreationResponse("STICKER_UPDATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponseBuilder.ErrorResponse("STICKER_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<StickerUpdateInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
