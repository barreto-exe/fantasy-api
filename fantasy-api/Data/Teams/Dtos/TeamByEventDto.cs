using FantasyApi.DataAnotations;

namespace FantasyApi.Data.Teams.Dtos
{
    public class TeamByEventDto
    {
        [DataNames("id_event")]
        public int EventId { get; set; }
        [DataNames("id_team")]
        public int TeamId { get; set; }
    }
}
