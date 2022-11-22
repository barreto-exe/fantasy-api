using FantasyApi.Data.Base.Requests;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Auth.Inputs
{
    public class LoginInput : BaseRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
