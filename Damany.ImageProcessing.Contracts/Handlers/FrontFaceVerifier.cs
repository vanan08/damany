using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Handlers
{
    public class FrontFaceVerifier : IOperation<Portrait>
    {
        public FrontFaceVerifier(string template)
        {
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentException("template can't be null", "template");
            }

            if (!System.IO.File.Exists(template))
            {
                throw new System.IO.FileNotFoundException("template file not found", template);
            }


            var ipl = OpenCvSharp.IplImage.FromFile(template);
            _verifier = new FaceProcessingWrapper.FrontFaceVerifier(ipl);
        }



        public IEnumerable<Portrait> Execute(IEnumerable<Portrait> inputs)
        {
            foreach (var portrait in inputs)
            {
                if (_verifier.IsFrontFace(portrait.GetIpl().GetSub(portrait.FaceBounds)))
                {
                    yield return portrait;
                }
                else
                {
                    portrait.Dispose();
                }
            }
        }

        private FaceProcessingWrapper.FrontFaceVerifier _verifier;

    }
}
