using RemoteImaging.RealtimeDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteImaging.Core;


namespace RemoteImaging.Test
{


    /// <summary>
    ///This is a test class for ImageClassifierTest and is intended
    ///to contain all ImageClassifierTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageClassifierTest
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
        ///A test for ClassifyImages
        ///</summary>
        [TestMethod()]
        public void ClassifyImagesTest()
        {
            string[] files = System.IO.Directory.GetFiles(@"d:\20090505");
            ImageDetail[] images = new ImageDetail[10];
            for (int i = 0; i < images.Length; i++)
            {
                ImageDetail d = ImageDetail.FromPath(files[i]);
                images[i] = d;
            }

            ImageClassifier.ClassifyImages(images);
        }

    }
}
