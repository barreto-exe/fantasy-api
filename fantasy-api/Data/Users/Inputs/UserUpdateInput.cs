using FantasyApi.Data.Base.Requests;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Users.Inputs
{
    public class UserUpdateInput : BaseRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
