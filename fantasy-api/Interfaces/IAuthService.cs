using FantasyApi.Data.Auth.Dtos;
using FantasyApi.Data.Auth.Inputs;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IAuthService
    {
        /// <exception cref="UserExistsException"></exception>
        Task<LoginDto> RegisterAsync(RegisterInput input);

        Task<LoginDto> LoginAsync(LoginInput input);
    }
}
