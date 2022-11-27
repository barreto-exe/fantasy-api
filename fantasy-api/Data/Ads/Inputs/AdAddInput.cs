using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Ads.Inputs
{
    public class AdAddInput : BaseRequest
    {
        [Required]
        public string Alias { get; set; }
        [Required]
        public string AdType { get; set; }
        [Required]
        public string RedirectTo { get; set; }
        public IFormFile Img { get; set; }
        public string Description { get; set; }
    }
}
