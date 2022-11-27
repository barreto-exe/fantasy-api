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

namespace FantasyApi.Functions.Teams
{
    public class DeleteTeams
    {
        private readonly ITeamsService _teamsService;
        public DeleteTeams(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [FunctionName("DeleteTeams")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "teams/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            async Task<IActionResult> Action(BaseRequest request)
            {
                try
                {
                    await _teamsService.DeleteTeamAsync(id);
                    return new OkObjectResult(ResponseBuilder.GeneralResponse("TEAM_DELETED"));
                }
                catch (NotFoundException)
                {
                    return new NotFoundObjectResult(ResponseBuilder.ErrorResponse("TEAM_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
