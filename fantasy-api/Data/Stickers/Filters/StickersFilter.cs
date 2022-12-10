using FantasyApi.Data.Base.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Stickers.Filters
{
    public class StickersFilter : BaseRequest
    {
        public int? EventId { get; set; }
        public int? TeamId { get; set; }
    }
}
