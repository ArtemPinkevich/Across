using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Handlers.Helpers
{
    public static class AdminLoginPrefixHelper
    {
        public const string ADMIN_LOGIN_PREFIX = "admin_";

        public static bool HasPrefix(string login)
        {
            throw new NotImplementedException();
        }

        public static string RemovePrefix(string login)
        {
            throw new NotImplementedException();
        }

        public static string AddPrefix(string login)
        {
            return ADMIN_LOGIN_PREFIX + login;
        }
    }
}
