using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public class Operations<T> : IOperation<T>
    {
        private readonly List<IOperation<T>> _operations = new List<IOperation<T>>();

        public Operations<T> Register(IOperation<T> operation)
        {
            _operations.Add(operation);
            return this;
        }


        #region IOperation<T> Members

        public IEnumerable<T> Execute(IEnumerable<T> input)
        {
            IEnumerable<T> current = input;
            foreach (var operation in _operations)
            {
                current = operation.Execute(current);
            }

            return current;
        }

        #endregion
    }
}
