using FantasyApi.Data.Base.Requests;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Users.Inputs
{
    public class UserAddInput : BaseRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
