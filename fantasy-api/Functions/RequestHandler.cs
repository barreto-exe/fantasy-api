using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Inputs;
using FantasyApi.Utils;
using FantasyApi.Utils.JWT;
using FantasyApi.Utils.JWT.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FantasyApi.Functions
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> Handle<TInput>(HttpRequest req, ILogger log, Func<TInput, Task<IActionResult>> function, RoleEnum role, BodyTypeEnum bodyType = BodyTypeEnum.Json)
            where TInput : BaseRequest, new()
        {
            //Validating jwt
            var jwt = new JWTValidator(req, role);
            if (!jwt.IsValid)
            {
                return new UnauthorizedObjectResult("Jwt is not valid.");
            }

            TInput input = null;
            try
            {
                // A base request has no aditional parameters 
                //if (typeof(TInput) == typeof(BaseRequest) || req.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    input = new();
                //}
                //else 

                if (req.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    input = new();
                }
                else if (req.Method.Equals("get", StringComparison.InvariantCultureIgnoreCase))
                {
                    input = req.Query.ToObject<TInput>();
                }
                else
                {
                    switch (bodyType)
                    {
                        case BodyTypeEnum.Json:
                            {
                                input = await req.Body.ToObjectAsync<TInput>();
                                if (input == null) throw new Exception("Request body is empty");
                                break;
                            }
                        case BodyTypeEnum.Formdata:
                            {
                                input = req.Form.ToObject<TInput>();

                                var files = req.Form.Files.ToList();
                                foreach (var file in files)
                                {
                                    var settings = BindingFlags.Instance |
                                        BindingFlags.Public |
                                        BindingFlags.SetProperty |
                                        BindingFlags.IgnoreCase;

                                    var prop = input.GetType().GetProperty(file.Name, settings);
                                    if (prop != null && prop.CanWrite)
                                    {
                                        prop.SetValue(input, file);
                                    }
                                }

                                break;
                            }
                    }
                }

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
