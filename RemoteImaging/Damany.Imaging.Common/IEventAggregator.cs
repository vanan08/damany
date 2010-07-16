using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IEventAggregator
    {
        void Subscribe(Action<Portrait> subscriber);
        void Publish(Portrait portrait);
    }
}
