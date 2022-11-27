using FantasyApi.Data.Base.Requests;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Ads.Inputs
{
    public class AdClickInput : BaseRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
