using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Utils.JWT.Enum
{
    public enum RoleEnum
    {
        Any,
        [Description("Administrador")]
        Admin,
        [Description("Usuario")]
        User,
        [Description("Anunciante")]
        Advertiser,
    }
}
