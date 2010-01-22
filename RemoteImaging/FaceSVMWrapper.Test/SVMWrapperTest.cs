using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace FaceSVMWrapper.Test
{
    using FaceSVMWrapper;

    [TestFixture]
    public class SVMWrapperTest
    {
        SVM svm;

        [SetUp]
        public void Setup()
        {
            svm = SVM.LoadFrom(@"C:\faceRecognition");
        }


        [Test]
        public void TrainTest()
        {
            svm.Train();
        }
    }
}
