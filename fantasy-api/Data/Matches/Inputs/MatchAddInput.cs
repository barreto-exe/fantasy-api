using FantasyApi.Data.Base.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyApi.Data.Matches.Inputs
{
    public class MatchAddInput : BaseRequest
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int TeamOneId { get; set; }
        [Required]
        public int TeamTwoId { get; set; }
        [Required]
        public DateTime MatchedAt { get; set; }
        public IFormFile MyFile { get; set; }
    }
}
