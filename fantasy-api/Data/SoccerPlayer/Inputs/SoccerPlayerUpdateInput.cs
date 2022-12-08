using FantasyApi.Data.Base.Requests;

namespace FantasyApi.Data.SoccerPlayer.Inputs
{
    public class SoccerPlayerUpdateInput : BaseRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
