using FantasyApi.DataAnotations;
using System;

namespace FantasyApi.Data.Events.Dtos
{
    public class EventDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("name")]
        public string Name { get; set; }
        [DataNames("img")]
        public string Img { get; set; }
        [DataNames("active")]
        public bool IsActive { get; set; }
        [DataNames("creation_date")]
        public DateTime CreatedAt { get; set; }
        [DataNames("update_date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
