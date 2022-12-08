using FantasyApi.DataAnotations;
using System;

namespace FantasyApi.Data.Stickers.Dto
{
    public class StickerDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("player_name")]
        public string PlayerName { get; set; }
        [DataNames("img_url")]
        public string Img { get; set; }
        [DataNames("height")]
        public double Height { get; set; }
        [DataNames("weight")]
        public double Weight { get; set; }
        [DataNames("id_team")]
        public int TeamId { get; set; }
        [DataNames("id_event")]
        public int EventId { get; set; }
        [DataNames("position")]
        public string Position { get; set; }
        [DataNames("appearance_rate")]
        public double AppearanceRate { get; set; }
        [DataNames("creation_date")]
        public DateTime CreatedAt { get; set; }
        [DataNames("udpate_date")]
        public DateTime UpdatedAt { get; set; }
    }
}
