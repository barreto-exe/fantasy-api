using FantasyApi.Data.Base.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Teams.Filters
{
    public class TeamsFilter : BaseRequest
    {
        public int? EventId { get; set; }
    }
}
