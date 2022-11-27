using FantasyApi.DataAnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
