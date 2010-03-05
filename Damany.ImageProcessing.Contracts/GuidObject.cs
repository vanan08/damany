using System;
using System.Collections.Generic;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public class GuidObject
    {
        public GuidObject()
        {
            this.Guid = System.Guid.NewGuid();
        }

        public Guid Guid
        {
            get;
            private set;
        }
    }
}
