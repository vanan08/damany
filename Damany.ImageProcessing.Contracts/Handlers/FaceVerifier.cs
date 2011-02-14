using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Handlers
{
    public class FaceVerifier : IFacePostFilter
    {
        readonly FaceProcessingWrapper.FaceVerifier _verifier = new FaceProcessingWrapper.FaceVerifier();
        public bool IsFace(Portrait portrait)
        {
            return _verifier.IsFace(portrait.GetIpl().GetSub(portrait.FaceBounds));
        }
    }
}
