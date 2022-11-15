using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data.Inputs
{
    public class LoginInput : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
