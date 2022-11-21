using FantasyApi.DataAnotations;
using Newtonsoft.Json;
using System;

namespace FantasyApi.Data.Users.Dtos
{
    public class UserDto
    {
        [DataNames("id")]
        public string Id { get; set; }
        [DataNames("name")]
        public string Name { get; set; }
        [DataNames("birth_date")]
        public DateTime? BirthDate { get; set; }
        [DataNames("email")]
        public string Email { get; set; }
        [JsonIgnore]
        [DataNames("pass")]
        public string PasswordMd5 { get; set; }
        [DataNames("role_name")]
        public string Role { get; set; }
        [DataNames("creation_date")]
        public DateTime CreatedDate { get; set; }
        [DataNames("update_date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
