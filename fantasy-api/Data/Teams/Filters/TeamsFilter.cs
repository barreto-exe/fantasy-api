using FantasyApi.Data.Base.Requests;

namespace FantasyApi.Data.Teams.Filters
{
    public class TeamsFilter : BaseRequest
    {
        public int? EventId { get; set; }
    }
}
