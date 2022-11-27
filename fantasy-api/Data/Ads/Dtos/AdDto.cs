using FantasyApi.Data.Base.Requests;
using FantasyApi.DataAnotations;
using System;

namespace FantasyApi.Data.Ads.Dtos
{
    public class AdDto : BaseRequest
    {
        [DataNames("id")]
        public int Id { get; set; }
        [DataNames("alias")]
        public string Alias { get; set; }
        [DataNames("type")]
        public string AdType { get; set; }
        [DataNames("redirect_to")]
        public string RedirectTo { get; set; }
        [DataNames("img_url")]
        public string Img { get; set; }
        [DataNames("description")]
        public string Description { get; set; }
        [DataNames("requested")]
        public int RequestedQuantities { get; set; }
        [DataNames("clicked")]
        public int ClickedQuantities { get; set; }
        [DataNames("creation_date")]
        public DateTime CreatedAt { get; set; }
        [DataNames("update_date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
