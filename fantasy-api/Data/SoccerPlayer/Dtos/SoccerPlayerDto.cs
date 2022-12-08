using FantasyApi.DataAnotations;

namespace FantasyApi.Data.SoccerPlayer.Dtos
{
    public class SoccerPlayerDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("name")]
        public string Name { get; set; }
    }
}
