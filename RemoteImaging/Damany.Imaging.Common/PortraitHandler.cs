using System.Collections.Generic;

namespace Damany.Imaging.Common
{
    public class PortraitHandler : IOperation<Portrait>
    {
        public IEnumerable<Portrait> Execute(IEnumerable<Portrait> inputs)
        {
            foreach (var portrait in inputs)
            {
                System.Diagnostics.Debug.WriteLine(portrait);
            }

            return inputs;
            
        }
    }
}