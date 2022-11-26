using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
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

        Task<UserDto> GetUserByIdAsync(int id);

        Task<IEnumerable<UserDto>> GetUsersAsync();

        Task<PaginatedListDto<UserDto>> GetUsersPaginatedAsync(BaseRequest filter);

        /// <exception cref="AlreadyExistsException"></exception>
        Task<UserDto> AddUserAsync(UserAddInput input);

        /// <exception cref="NotFoundException"></exception>
        Task<UserDto> UpdateUserAsync(UserUpdateInput input);

        /// <exception cref="NotFoundException"></exception>
        Task DeleteUserAsync(int id);
    }
}
