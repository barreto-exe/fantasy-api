using System.ComponentModel;

namespace FantasyApi.Utils.JWT.Enum
{
    public enum RoleEnum
    {
        Any,
        [Description("Admin")]
        Admin,
        [Description("User")]
        User,
        [Description("Announcer")]
        Advertiser,
    }
}
