using FantasyApi.Data.Ads.Inputs;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Matches.Inputs;
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
    public class PutMatches
    {
        private readonly IMatchService _matchService;
        public PutMatches(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [FunctionName("PutMatches")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "games/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(MatchUpdateInput input)
            {
                try
                {
                    input.Id = id;
                    var result = await _matchService.UpdateMatchAsync(input);
                    return new OkObjectResult(ResponseBuilder.CreationResponse("MATCH_UPDATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponseBuilder.ErrorResponse("MATCH_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<MatchUpdateInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
