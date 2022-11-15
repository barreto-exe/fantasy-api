using Core.Utils.Mapping;
using FantasyApi.Data.Dtos;
using FantasyApi.Data.Inputs;
using FantasyApi.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseDatabaseService _baseDatabaseService;
        public AuthService(IBaseDatabaseService baseDatabaseService)
        {
            _baseDatabaseService = baseDatabaseService;
        }

        public async Task<LoginDto> Login(LoginInput input)
        {
            var user = await FindByNameAndPassAsync(input);
            if(user == null)
            {
                //Doesn't exist or wrong pass
                return null;
            }

            var authClaims = new List<Claim>()
            {
                new Claim("email", user.Email),
                new Claim("role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);

            return new LoginDto()
            {
                User = user,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

        private static JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<UserDto> FindByNameAndPassAsync(LoginInput input)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("vEmail", input.Email),
                new MySqlParameter("vPass", input.Password),
            };

            var cmd = _baseDatabaseService.GetCommand("GetUserByMailAndPass", parameters);
            var data = await _baseDatabaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data.Rows[0]);

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
