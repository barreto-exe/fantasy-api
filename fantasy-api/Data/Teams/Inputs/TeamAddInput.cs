using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Teams.Inputs
{
    public class TeamAddInput : BaseRequest
    {
        [Required]
        public string Name { get; set; }
        public IFormFile Badge { get; set; }
        [Required]
        public string EventIds { get; set; }
    }
}