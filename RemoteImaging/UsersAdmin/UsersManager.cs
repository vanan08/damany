using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Damany.Security.UsersAdmin
{
    public class UsersManager
    {
        private BindingList<User> usersCollection;
        private const string usersPersistFile = "users";

        public UsersManager()
        {
            usersCollection = new BindingList<User>();
        }

        public static UsersManager LoadUsers()
        {
            using (var stream = System.IO.File.OpenRead(usersPersistFile))
            {
                var formatter = GetFormatter();
                var users = (BindingList<User>)formatter.Deserialize(stream);
                var manager = new UsersManager();
                manager.usersCollection = users;

                return manager;

            }
        }

        private static IFormatter GetFormatter()
        {
            var formatter = new BinaryFormatter();
            return formatter;
        }

        public void Save()
        {
            using (var stream = System.IO.File.Open(usersPersistFile, System.IO.FileMode.Truncate))
            {
                var formatter = GetFormatter();
                formatter.Serialize(stream, usersCollection);
            }
            
        }

        private void MakeSureUserNotExists(User user)
        {
            if (this.usersCollection.Contains(user))
                throw new InvalidOperationException("user with name already exists");
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "user is null.");

            MakeSureUserNotExists(user);

            this.usersCollection.Add(user);

        }

        public bool UserNameExists(string userName)
        {
            return this.usersCollection.FirstOrDefault(user => user.Name.Equals(userName)) != null; 
        }


        public void DeleteUser(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("userName is null or empty.", "userName");

            if (!UserNameExists(userName))
            {
                throw new InvalidOperationException("user name doesn't exist");
            }

            var userToRemove = this[userName];

            this.usersCollection.Remove(userToRemove);

        }

        public User this[string name]
        {
            get
            {
                if (!this.UserNameExists(name))
                {
                    return null;
                }

                return this.usersCollection.First( item => item.Name.Equals(name) );
            }
            
        }



        private bool IsValidUser(string userName, string password)
        {
            if (!this.UserNameExists(userName))
                return false;

            User usr = this[userName];

            if (string.Compare(usr.Password, password, false) != 0)
                return false;

            return true;
        }

        public bool ChangePassword(string userName, string oldPwd, string newPwd)
        {
            if (!IsValidUser(userName, oldPwd))
                return false;

            User user = this[userName];
            user.Password = newPwd;
            return true;
        }


        public BindingList<User> Users
        {
            get
            {
                return this.usersCollection;
            }
        }

        public User GetUser(string name, string password)
        {
            if (!IsValidUser(name, password))
            {
                return null;
            }

            return this[name];

        }

    }
}
