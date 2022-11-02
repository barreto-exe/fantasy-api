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
using System.Data;

namespace FantasyApi.Functions
{
    public class Function1
    {
        private readonly IBaseDatabaseService _baseDatabaseService;
        public Function1(IBaseDatabaseService baseDatabaseService)
        {
            _baseDatabaseService = baseDatabaseService;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string query = "select * from entidades";
            var cmd = _baseDatabaseService.GetCommand(query, type: CommandType.Text);
            var data = await _baseDatabaseService.ExecuteStoredProcedureDataSetAsync(cmd);

            object result;

            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                result = data.Tables[0];
            }
            else
            {
                return null;
            }

            return new OkObjectResult(result);
        }
    }
}
