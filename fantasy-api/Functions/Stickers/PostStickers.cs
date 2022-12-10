using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Stickers.Inputs;
using FantasyApi.Data.Users.Inputs;
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
    public class PostStickers
    {
        private readonly IStickerService _stickerService;
        public PostStickers(IStickerService stickerService)
        {
            _stickerService = stickerService;
        }

        [FunctionName("PostStickers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "stickers")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(StickerAddInput input)
            {
                try
                {
                    var result = await _stickerService.AddStickerAsync(input);
                    return new OkObjectResult(ResponseBuilder.CreationResponse("STICKER_CREATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponseBuilder.ErrorResponse("ID_NOT_FOUND"));
                }
            }

            return await RequestHandler.Handle<StickerAddInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
