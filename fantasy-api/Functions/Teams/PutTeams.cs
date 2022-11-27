using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Events.Inputs;
using FantasyApi.Utils.JWT.Enum;
using FantasyApi.Utils;
using FantasyApi.Data.Teams.Inputs;
using FantasyApi.Interfaces;

namespace FantasyApi.Functions.Teams
{
    public class PutTeams
    {
        private readonly ITeamsService _teamService;
        public PutTeams(ITeamsService teamService)
        {
            _teamService = teamService;
        }

        [FunctionName("PutTeams")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "teams/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(TeamUpdateInput input)
            {
                try
                {
                    input.Id = id;
                    var result = await _teamService.UpdateTeamAsync(input);
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("TEAM_UPDATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("TEAM_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<TeamUpdateInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
