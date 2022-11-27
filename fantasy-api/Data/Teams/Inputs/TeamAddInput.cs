using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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