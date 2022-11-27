using FantasyApi.DataAnotations;
using System;
using System.Collections.Generic;

namespace FantasyApi.Data.Teams.Dtos
{
    public class TeamDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("name")]
        public string Name { get; set; }
        public IEnumerable<int> EventIds { get; set; }
        [DataNames("creation_date")]
        public DateTime CreatedAt { get; set; }
        [DataNames("update_date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
