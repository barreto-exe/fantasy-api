using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
