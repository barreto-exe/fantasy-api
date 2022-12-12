using FantasyApi.Data.Events.Dtos;
using FantasyApi.Data.Teams.Dtos;
using FantasyApi.DataAnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FantasyApi.Data.Matches.Dtos
{
    public class MatchDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [JsonIgnore]
        [DataNames("id_team1")]
        public int TeamOneId { get; set; }
        public TeamDto TeamOne { get; set; }
        [JsonIgnore]
        [DataNames("id_team2")]
        public int TeamTwoId { get; set; }
        public TeamDto TeamTwo { get; set; }
        [JsonIgnore]
        [DataNames("id_event")]
        public int EventId { get; set; }
        public EventDto Event { get; set; }
        [DataNames("date")]
        public DateTime MatchedAt { get; set; }
        [DataNames("file_url")]
        public string MyFile { get; set; }
        public IEnumerable<PlayerByMatchDto> Players { get; set; }
    }
}
