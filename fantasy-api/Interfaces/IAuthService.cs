using FantasyApi.Data.Auth.Dtos;
using FantasyApi.Data.Auth.Inputs;
using System.Threading.Tasks;
using FantasyApi.Data.Base.Exceptions;

namespace FantasyApi.Interfaces
{
    public interface IAuthService
    {
        /// <exception cref="AlreadyExistsException"></exception>
        Task<LoginDto> RegisterAsync(RegisterInput input);

        Task<LoginDto> LoginAsync(LoginInput input);
    }
}
