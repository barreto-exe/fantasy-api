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
    public class PostMatches
    {
        private readonly IMatchService _matchService;
        public PostMatches(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [FunctionName("PostMatches")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "games")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(MatchAddInput input)
            {
                try
                {
                    var result = await _matchService.AddMatchAsync(input);
                    return new OkObjectResult(ResponseBuilder.CreationResponse("MATCH_CREATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponseBuilder.ErrorResponse("ID_NOT_FOUND"));
                }
            }

            return await RequestHandler.Handle<MatchAddInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
