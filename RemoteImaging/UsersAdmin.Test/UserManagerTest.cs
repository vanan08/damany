using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Damany.Security.UsersAdmin;
using System.IO;
using System.Diagnostics;

namespace UsersAdmin.Test
{
    [TestFixture]
    class UserManagerTest
    {
        [SetUp]
        public void Setup()
        {
            string file = @"users";

            File.WriteAllBytes(file, new byte[0]);

        }

        [Test]
        public void SerialDeserialTest()
        {
            UsersManager mnger = new UsersManager();

            var newUser = new User("admin", "admin");
            newUser.Roles.Add("admin");
            mnger.AddUser(newUser);

            mnger.Save();

            UsersManager saved = UsersManager.LoadUsers();


            foreach (var user in saved.Users)
            {
                Trace.Write(string.Format("name: {0}, pwd: {1}", user.Name, user.Password));
            }


            Assert.IsNotNull(saved["admin"]);
            Assert.IsNull(saved["abc"]);

        }
    }
}
