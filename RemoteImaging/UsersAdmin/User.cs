using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Damany.Security.UsersAdmin
{
    [Serializable]
    public class User
    {
        public User(string name, string pwd)
        {
            this.Name = name;
            this.Password = pwd;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        private IList<string> roles = new List<string>();
        public IList<string> Roles
        {
            get
            {
                return roles;
            }
        }

        public IPrincipal ToPrincipal()
        {
            return new GenericPrincipal(
                                new GenericIdentity(this.Name),
                                this.roles.ToArray());
        }
    }
}
