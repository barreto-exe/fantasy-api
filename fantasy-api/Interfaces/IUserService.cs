using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Users.Dtos;
using FantasyApi.Data.Users.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByMailAndPassAsync(LoginInput input);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        /// <exception cref="UserExistsException"></exception>
        Task AddUserAsync(UserAddInput input);
    }
}
