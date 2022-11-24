using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Events.Inputs
{
    public class EventAddInput : BaseRequest
    {
        [Required]
        public string EventName { get; set; }
        public IFormFile Img { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
