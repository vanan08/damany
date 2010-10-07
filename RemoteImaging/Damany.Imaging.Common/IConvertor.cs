using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IConvertor<TInput, TOutput>
    {
        IEnumerable<TOutput> Execute(IEnumerable<TInput> inputs);
    }
}
