using System;

namespace Damany.Imaging.Common
{
    public class NullEventAggregator : IEventAggregator
    {
        public void Subscribe(Action<Portrait> subscriber)
        {
        }

        public void Publish(Portrait portrait)
        {
        }
    }
}