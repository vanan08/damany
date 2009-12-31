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
        public static bool IsCurrentUserAllowedToProceed()
        {
            IAuthorizationProvider authorizeProvider =
                AuthorizationFactory.GetAuthorizationProvider("RuleProvider");

            bool canProceed = authorizeProvider.Authorize(System.Threading.Thread.CurrentPrincipal, "ConfigSystem");
            return canProceed;
        }
    }
}
