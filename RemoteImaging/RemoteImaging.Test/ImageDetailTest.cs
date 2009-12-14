using RemoteImaging.RealtimeDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RemoteImaging.Core;

namespace RemoteImaging.Test
{


    /// <summary>
    ///This is a test class for ImageDetailTest and is intended
    ///to contain all ImageDetailTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageDetailTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ImageDetail Constructor
        ///</summary>
        [TestMethod()]
        public void ImageDetailConstructorTest()
        {
            string pathName = @"c:\abc\02_090426154606-0011.jpg"; // TODO: Initialize to an appropriate value
            ImageDetail target = ImageDetail.FromPath(pathName);

            Assert.AreEqual(2, target.FromCamera);
            Assert.AreEqual(2009, target.CaptureTime.Year);
            Assert.AreEqual(4, target.CaptureTime.Month);
            Assert.AreEqual(26, target.CaptureTime.Day);
            Assert.AreEqual(15, target.CaptureTime.Hour);
            Assert.AreEqual(46, target.CaptureTime.Minute);
            Assert.AreEqual(6, target.CaptureTime.Second);

            Assert.AreEqual(@"c:\abc", target.ContainedBy);
            Assert.AreEqual(@"02_090426154606-0011.jpg", target.Name);
            Assert.AreEqual(@"c:\abc\02_090426154606-0011.jpg", target.Path);
        }
    }
}
