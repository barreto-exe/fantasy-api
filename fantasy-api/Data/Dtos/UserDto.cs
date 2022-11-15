using FantasyApi.DataAnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Dtos
{
    public class UserDto
    {
        [DataNames("idUsuario")]
        public string Id { get; set; }
        [DataNames("nombre")]
        public string FirstName { get; set; }
        [DataNames("apellido")]
        public string LastName { get; set; }
        [DataNames("email")]
        public string Email { get; set; }
        [DataNames("pass")]
        [JsonIgnore]
        public string PasswordMd5 { get; set; }
        [DataNames("rol")]
        public string Role { get; set; }
        [DataNames("fecha_nac")]
        public DateTime BirthDate { get; set; }
        [DataNames("fecha_creacion")]
        public DateTime CreatedDate { get; set; }
        [DataNames("fecha_actualizacion")]
        public DateTime? UpdatedDate { get; set; }
    }
}
