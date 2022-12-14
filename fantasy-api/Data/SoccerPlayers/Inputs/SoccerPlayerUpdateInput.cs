using FantasyApi.Data.Base.Requests;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.SoccerPlayer.Inputs
{
    public class SoccerPlayerUpdateInput : BaseRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalUuid { get; set; }
    }
}
