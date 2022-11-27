using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FantasyApi.Interfaces;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Services;
using FantasyApi.Utils.JWT.Enum;
using FantasyApi.Data.Teams.Filters;

namespace FantasyApi.Functions.Teams
{
    public class GetAllTeams
    {
        private readonly ITeamsService _teamService;
        public GetAllTeams(ITeamsService teamService)
        {
            _teamService = teamService;
        }

        [FunctionName("GetAllTeams")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "teams/all")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(TeamsFilter filter)
            {
                var data = await _teamService.GetTeamsAsync(filter);
                return new OkObjectResult(data);
            }

            return await RequestHandler.Handle<TeamsFilter>(req, log, Action, RoleEnum.Admin);
        }
    }
}
