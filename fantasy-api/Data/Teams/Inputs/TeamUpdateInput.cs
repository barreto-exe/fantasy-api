using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Teams.Inputs
{
    public class TeamUpdateInput : BaseRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Badge { get; set; }
        public string EventIds { get; set; }
    }
}
