using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.ViewModels;

namespace WebLogic.Security
{
    public static class CustomClaimTypes
    {
        public const string Permission = "user.permission";
    }
    public class SecurityService
    {
        public List<UserViewModel> GetUsers()
        {
            return null;
        }
    }
}
