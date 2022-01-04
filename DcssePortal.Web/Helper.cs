using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DcssePortal.Web
{
    public static class Helper
    {
        public static eUserRoles CurrentUserRole { get; set; }
    }

    public enum eUserRoles
    {
        Admin=1,
        Faculty=2,
        Student=3
    }
}