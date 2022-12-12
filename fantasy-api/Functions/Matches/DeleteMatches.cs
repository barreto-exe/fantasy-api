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

namespace FantasyApi.Functions.Matches
{
    public class DeleteMatches
    {
        private readonly IMatchService _matchService;
        public DeleteMatches(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [FunctionName("DeleteMatches")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "games/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest request)
            {
                try
                {
                    await _matchService.DeleteMatchAsync(id);
                    return new OkObjectResult(ResponseBuilder.GeneralResponse("MATCH_DELETED"));
                }
                catch (NotFoundException)
                {
                    return new NotFoundObjectResult(ResponseBuilder.ErrorResponse("MATCH_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
