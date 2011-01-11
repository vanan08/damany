using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IConfigurable
    {
        object GetConfig();
        void SetConfig(object config);
    }
}
