using FantasyApi.Data.Users.Dtos;

namespace FantasyApi.Data.Auth.Dtos
{
    public class LoginDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
