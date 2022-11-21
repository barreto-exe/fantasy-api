using FantasyApi.Data.Auth.Dtos;
using FantasyApi.Data.Auth.Inputs;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IAuthService
    {
        /// <exception cref="UserExistsException"></exception>
        Task<LoginDto> Register(RegisterInput input);

        Task<LoginDto> Login(LoginInput input);

        public static ErrorDto ErrorCodeBuilder(string errorCode)
        {
            return new ErrorDto()
            {
                Code = errorCode,
            };
        }
    }
}
