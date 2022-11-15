using FantasyApi.Data.Dtos;
using FantasyApi.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IAuthService
    {
        Task<LoginDto> Login(LoginInput input);
        Task<UserDto> FindByNameAndPassAsync(LoginInput input);
    }
}
