using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Users.Dtos;
using FantasyApi.Data.Users.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByMailAndPassAsync(LoginInput input);

        Task<UserDto> GetUserById(int id);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<PaginatedListDto<UserDto>> GetAllUsersPaginatedAsync(BaseRequest filter);

        /// <exception cref="UserExistsException"></exception>
        Task<UserDto> AddUserAsync(UserAddInput input);

        /// <exception cref="UserDoesntExistException"></exception>
        Task DeleteUserAsync(int id);
    }
}
