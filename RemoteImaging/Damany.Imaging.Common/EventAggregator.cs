using System;
using System.Collections.Concurrent;

namespace Damany.Imaging.Common
{
    public class EventAggregator : IEventAggregator
    {
        readonly BlockingCollection<Action<Portrait>> _subscribers =
            new BlockingCollection<Action<Portrait>>();

        public void Subscribe(Action<Portrait> subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Publish(Portrait portrait)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber(portrait);
            }
        }
    }
}