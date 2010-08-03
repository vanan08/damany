using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchCamera
{
    public class ParseResult<T>
    {
        public bool Handled { get; private set; }
        public T Data { get; private set; }

        public ParseResult(bool handled, T data)
        {
            Handled = handled;
            Data = data;
        }
    }
}
