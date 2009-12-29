using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Damany.Security.UsersAdmin
{
    public class UsersManager
    {
        private Dictionary<string, User> usersCollection;

        public UsersManager()
        {
            usersCollection = new Dictionary<string, User>();
        }

        public static UsersManager LoadUsers(System.IO.Stream from)
        {
            var formatter = GetFormatter();
            var users = (Dictionary<string, User>)  formatter.Deserialize(from);
            var manager = new UsersManager();
            manager.usersCollection = users;

            return manager;
        }

        private static IFormatter GetFormatter()
        {
            var formatter = new BinaryFormatter();
            return formatter;
        }

        public void Save(System.IO.Stream to)
        {
            var formatter = GetFormatter();
            formatter.Serialize(to, usersCollection);
        }

        private void MakeSureUserNotExists(User user)
        {
            if (this.usersCollection.ContainsKey(user.Name))
                throw new InvalidOperationException("user with name already exists");
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "user is null.");

            MakeSureUserNotExists(user);

            this.usersCollection.Add(user.Name, user);

        }

        private void MakeSureUserExists(string userName)
        {
            if (this.usersCollection.ContainsKey(userName))
            {
                throw new InvalidOperationException("user doesn't exists");
            }
        }

        public void DeleteUser(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("userName is null or empty.", "userName");

            MakeSureUserExists(userName);

            this.usersCollection.Remove(userName);

        }

        public User this[string name]
        {
            get
            {
                if (this.usersCollection.ContainsKey(name))
                {
                    return this.usersCollection[name];
                }

                return null;
            }
            
        }



        private void verifyPassword(string userName, string password)
        {
            MakeSureUserExists(userName);

            User usr = this[userName];

            if (string.Compare(usr.Password, password, false) != 0)
            {
                throw new InvalidOperationException("password is not valid");
            }
        }

        public void ChangePassword(string userName, string oldPwd, string newPwd)
        {
            MakeSureUserExists(userName);

            User user = this[userName];

            verifyPassword(userName, oldPwd);
            user.Password = newPwd;
        }

        public User GetUser(string name, string password)
        {
            try
            {
                this.MakeSureUserExists(name);
                this.verifyPassword(name, password);
                return this[name];
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

        }

    }
}
