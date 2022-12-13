using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Stickers.Inputs
{
    public class StickerAddInput : BaseRequest
    {
        [Required]
        public string PlayerName { get; set; }
        public IFormFile Img { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string ExternalUuid { get; set; }
        [Required]
        public double AppearanceRate { get; set; }
    }
}
