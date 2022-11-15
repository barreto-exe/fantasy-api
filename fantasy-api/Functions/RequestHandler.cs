using FantasyApi.Data;
using FantasyApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FantasyApi.Functions
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> Handle<TInput>(HttpRequest req, ILogger log, Func<TInput, Task<IActionResult>> function)
            where TInput : BaseRequest, new()
        {
            //Validating jwt
            //var jwt = new JWTValidator(req);
            //if (!jwt.IsValid)
            //{
            //    return new BadRequestObjectResult("Jwt is not valid.");
            //}

            TInput input;
            try
            {
                // A base request has no aditional parameters 
                if (typeof(TInput) == typeof(BaseRequest) || req.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    input = new();
                }
                else if (req.Method.Equals("get", StringComparison.InvariantCultureIgnoreCase))
                {
                    input = req.Query.ToObject<TInput>();
                }
                else
                {
                    input = await req.Body.ToObjectAsync<TInput>();
                    if (input == null) throw new Exception("Request body is empty");
                }

                //input.Username = jwt.GetUsername();

                if (!input.IsValid(out List<ValidationResult> validationResults))
                {
                    return new BadRequestObjectResult(validationResults);
                }

                return await function.Invoke(input);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("An error has occurred: " + ex.Message);
            }
        }
    }
}
