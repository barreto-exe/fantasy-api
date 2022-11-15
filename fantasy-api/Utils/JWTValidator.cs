using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace FantasyApi.Utils
{
    public class JWTValidator
    {
        public bool IsValid { get; }
        public IDictionary<string, object> Claims { get; set; }

        public JWTValidator(HttpRequest request)
        {
            // Check if we have a header.
            if (!request.Headers.ContainsKey("Authorization"))
            {
                IsValid = false;
                return;
            }

            string authorizationHeader = request.Headers["Authorization"];

            // Check if the value is empty.
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                IsValid = false;
                return;
            }

            try
            {
                if (authorizationHeader.StartsWith("Bearer"))
                {
                    authorizationHeader = authorizationHeader.Substring(7);
                }

                //Read JWT Public key data
                string modulus = Environment.GetEnvironmentVariable("JWTK_RSA_MODULUS");
                string exponent = Environment.GetEnvironmentVariable("JWTK_RSA_EXPONENT");

                RSACryptoServiceProvider publicKey = new RSACryptoServiceProvider();
                publicKey.ImportParameters(
                  new RSAParameters()
                  {
                      Modulus = FromBase64Url(modulus),
                      Exponent = FromBase64Url(exponent)
                  });

                Claims =
                    new JwtBuilder()
                    .WithAlgorithm(new RS256Algorithm(publicKey))
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(authorizationHeader);
            }
            catch (Exception exception)
            {
                IsValid = false;
                return;
            }

            //if (!_claims.ContainsKey("emails"))
            //{
            //    IsValid = false;
            //    return;
            //}

            //if (!_claims.ContainsKey("extension_Username"))
            //{
            //    IsValid = false;
            //    return;
            //}

            IsValid = true;
        }

        private byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
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