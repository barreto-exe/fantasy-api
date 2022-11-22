using FantasyApi.Utils.JWT.Enum;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyApi.Utils.JWT
{
    public class JWTValidator
    {
        public IDictionary<string, object> Claims { get; set; }
        public bool IsValid { get; set; } = false;

        public JWTValidator(HttpRequest request, RoleEnum role)
        {
            //If auth's for any, then there's nothing to check
            if (role == RoleEnum.Any)
            {
                IsValid = true;
                return;
            }

            // Check if we have a header
            if (!request.Headers.ContainsKey("Authorization"))
            {
                return;
            }

            // Check if the value is empty
            string authorizationHeader = request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return;
            }

            //Check signature
            try
            {
                if (authorizationHeader.StartsWith("Bearer"))
                {
                    authorizationHeader = authorizationHeader.Substring(7);
                }

                string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
                Claims =
                    new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(jwtSecret)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(authorizationHeader);
            }
            catch
            {
                return;
            }

            //Check role
            string userRole = GetValueOrDefault("role").ToString().ToLower();
            string actionRole = role.GetDescription().ToLower();
            if (userRole != actionRole)
            {
                return;
            }

            //Everything OK
            IsValid = true;
        }

        public object GetValueOrDefault(string key)
        {
            var exists = Claims.TryGetValue(key, out object value);
            if (exists)
            {
                var converted = value as JArray ?? value as JToken;
                if (converted is JArray)
                {
                    return converted.ToObject<List<string>>();
                }
                else if (converted is JToken)
                {
                    return converted.ToObject<string>().FirstOrDefault();
                }

                return Claims.FirstOrDefault(x => x.Key == key).Value;
            }
            return null;
        }
    }
}