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
    public class FacePCATest
    {
        [Test]
        public void Test()
        {
            FaceProcessingWrapper.PCA pca = FaceProcessingWrapper.PCA.LoadFrom(@"c:\facerecognition");
            string s = pca.GetFileName(1);
        }
    }
}
