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
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Utils.JWT.Enum;
using FantasyApi.Utils;

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
                    return new OkObjectResult(ResponsesBuilder.DeletionResponse("TEAM_DELETED"));
                }
                catch (NotFoundException)
                {
                    return new NotFoundObjectResult(ResponsesBuilder.ErrorResponse("TEAM_DOESNT_EXIST"));
                }
            }

            return await RequestHandler.Handle<BaseRequest>(req, log, Action, RoleEnum.Admin);
        }
    }
}
