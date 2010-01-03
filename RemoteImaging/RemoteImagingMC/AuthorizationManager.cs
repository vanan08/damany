using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security;
using System.Windows.Forms;

namespace RemoteImaging
{
    public class AuthorizationManager
    {
        public static bool IsCurrentUserAuthorized()
        {
            IAuthorizationProvider authorizeProvider =
                AuthorizationFactory.GetAuthorizationProvider("RuleProvider");

            bool authorized = authorizeProvider.Authorize(System.Threading.Thread.CurrentPrincipal, "ConfigSystem");
            return authorized;
        }
    }
}
