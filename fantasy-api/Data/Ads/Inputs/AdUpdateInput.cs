using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Ads.Inputs
{
    public class AdUpdateInput : BaseRequest
    {
        [Required]
        public int Id { get; set; }
        public string Alias { get; set; }
        public string AdType { get; set; }
        public string RedirectTo { get; set; }
        public IFormFile Img { get; set; }
        public string Description { get; set; }
    }
}
