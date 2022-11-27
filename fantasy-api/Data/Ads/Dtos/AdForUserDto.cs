using FantasyApi.Data.Base.Requests;
using FantasyApi.DataAnotations;

namespace FantasyApi.Data.Ads.Dtos
{
    public class AdForUserDto
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("redirect_to")]
        public string RedirectTo { get; set; }
        [DataNames("img_url")]
        public string Img { get; set; }
        [DataNames("description")]
        public string Description { get; set; }
    }
}
