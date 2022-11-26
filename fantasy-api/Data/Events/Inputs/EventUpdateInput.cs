using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;

namespace FantasyApi.Data.Events.Inputs
{
    public class EventUpdateInput : BaseRequest
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public IFormFile Img { get; set; }
        public bool? Active { get; set; }
    }
}
