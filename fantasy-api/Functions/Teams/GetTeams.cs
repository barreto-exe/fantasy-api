using FantasyApi.Data.Teams.Filters;
using FantasyApi.Interfaces;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Teams
{
    public class GetTeams
    {
        private readonly ITeamsService _teamsService;
        public GetTeams(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [FunctionName("GetTeams")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "teams")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(TeamsFilter filter)
            {
                var data = await _teamsService.GetTeamsPaginatedAsync(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<TeamsFilter>(req, log, Action, RoleEnum.Admin);
        }
    }
}
