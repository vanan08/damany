using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Handlers
{
    public class FaceVerifier : IOperation<Portrait>
    {

        public IEnumerable<Portrait> Execute(IEnumerable<Portrait> inputs)
        {
            foreach (var portrait in inputs)
            {
                if (_verifier.IsFace(portrait.GetIpl()))
                {
                    yield return portrait;
                }
                else
                {
                    portrait.Dispose();
                }
            }
            
        }

        FaceProcessingWrapper.FaceVerifier _verifier = new FaceProcessingWrapper.FaceVerifier();
    }
}
