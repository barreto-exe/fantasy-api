using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Utils.JWT.Enum;
using FantasyApi.Services;
using FantasyApi.Interfaces;
using FantasyApi.Data.Teams.Filters;

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
