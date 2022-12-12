using FantasyApi.Data.Base.Requests;
using FantasyApi.Interfaces;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Matches
{
    public class GetMatches
    {
        private readonly IMatchService _matchService;
        public GetMatches(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [FunctionName("GetMatches")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "games")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest filter)
            {
                var data = await _matchService.GetMatchesPaginatedAsync(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
