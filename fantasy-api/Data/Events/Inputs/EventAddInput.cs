using FantasyApi.Data.Base.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Events.Inputs
{
    public class EventAddInput : BaseRequest
    {
        public string EventName { get; set; }
        public string Img { get; set; }
        public bool Active { get; set; }
    }
}
