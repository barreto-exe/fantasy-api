using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Matches.Inputs
{
    public class MatchUpdateInput : BaseRequest
    {
        [Required]
        public int Id;
        public int? EventId { get; set; }
        public int? TeamOneId { get; set; }
        public int? TeamTwoId { get; set; }
        public DateTime? MatchedAt { get; set; }
        public IFormFile MyFile { get; set; }
    }
}
