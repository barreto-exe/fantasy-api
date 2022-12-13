using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Stickers.Inputs
{
    public class StickerUpdateInput : BaseRequest
    {
        [Required]
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public IFormFile Img { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public int? TeamId { get; set; }
        public int? EventId { get; set; }
        public string Position { get; set; }
        public string ExternalUuid { get; set; }
        public double? AppearanceRate { get; set; }
    }
}
