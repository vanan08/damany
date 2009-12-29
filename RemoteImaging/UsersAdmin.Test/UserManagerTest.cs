using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Damany.Security.UsersAdmin;
using System.IO;

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
            mnger.AddUser(new User("jack", "admin"));

            using (FileStream stream = File.OpenWrite("users"))
            {
                mnger.Save(stream);
            }

            using (FileStream stream = File.OpenRead("users"))
            {
                UsersManager saved = UsersManager.LoadUsers(stream);
                Assert.IsNotNull(saved["jack"]);
            }

        }
    }
}
