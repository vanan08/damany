using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public interface IPortraitHandler
    {
        void HandlePortraits(IList<Portrait> portraits);
    }
}
