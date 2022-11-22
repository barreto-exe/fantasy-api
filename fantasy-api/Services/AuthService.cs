using FantasyApi.Data.Auth.Dtos;
using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Users.Exceptions;
using FantasyApi.Data.Users.Inputs;
using FantasyApi.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabaseService _baseDatabaseService;
        private readonly IUserService _userService;
        public AuthService(IDatabaseService baseDatabaseService, IUserService userService)
        {
            _baseDatabaseService = baseDatabaseService;
            _userService = userService;
        }

        /// <exception cref="UserExistsException"></exception>
        public async Task<LoginDto> Register(RegisterInput input)
        {
            try
            {
                await _userService.AddUserAsync(new UserAddInput
                {
                    Name = input.Name,
                    Role = "User",
                    BirthDate = input.BirthDate,
                    Email = input.Email,
                    Password = input.Password,
                });
            }
            catch (UserExistsException ex)
            {
                throw ex;
            }

            return await Login(new LoginInput
            {
                Email = input.Email,
                Password = input.Password,
            });
        }

        public async Task<LoginDto> Login(LoginInput input)
        {
            var user = await _userService.GetUserByMailAndPassAsync(input);
            if (user == null)
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
    }
}
