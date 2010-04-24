using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IOperation<T>
    {
        IEnumerable<T> Execute(IEnumerable<T> inputs);
    }
}
