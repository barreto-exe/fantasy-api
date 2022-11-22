using FantasyApi.Data.Base.Requests;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Auth.Inputs
{
    public class RegisterInput : BaseRequest
    {
        [Required]
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
