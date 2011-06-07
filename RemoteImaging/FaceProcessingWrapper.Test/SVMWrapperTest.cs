using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace FaceProcessingWrapper.Test
{
    using FaceProcessingWrapper;

    [TestFixture]
    public class SVMWrapperTest
    {
        //SVM svm;

        [SetUp]
        public void Setup()
        {
            //svm = SVM.LoadFrom(@"C:\faceRecognition");
        }


        [Test]
        public void TrainTest()
        {
        }
    }
}
