using FantasyApi.DataAnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Matches.Dtos
{
    public class PlayerByMatchDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("name")]
        public string Name { get; set; }
        [DataNames("id_team")]
        public int TeamId { get; set; }
        [DataNames("id_external")]
        public string Identifier { get; set; }
        [DataNames("points")]
        public double Points { get; set; }
    }
}
