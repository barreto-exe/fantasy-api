using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Teams.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyApi.Functions.Teams
{
    public class PostTeams
    {
        private readonly ITeamsService _teamService;
        public PostTeams(ITeamsService teamService)
        {
            _teamService = teamService;
        }

        [FunctionName("PostTeams")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "teams")] HttpRequest req,
            ILogger log)
        {
            async Task<IActionResult> Action(TeamAddInput input)
            {
                try
                {
                    var result = await _teamService.AddTeamAsync(input);
                    return new OkObjectResult(ResponsesBuilder.CreationResponse("TEAM_CREATED", result));
                }
                catch (NotFoundException)
                {
                    return new BadRequestObjectResult(ResponsesBuilder.ErrorResponse("EVENT_NOT_FOUND"));
                }
            }

            return await RequestHandler.Handle<TeamAddInput>(req, log, Action, RoleEnum.Admin, BodyTypeEnum.Formdata);
        }
    }
}
