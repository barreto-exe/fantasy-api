using FantasyApi.Data.Base.Requests;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.SoccerPlayer.Inputs
{
    public class SoccerPlayerAddInput : BaseRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ExternalUuid { get; set; }
    }
}
