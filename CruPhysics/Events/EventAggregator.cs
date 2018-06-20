using System.Collections.Generic;

namespace CruPhysics.Events
{
    internal class EventAggregator : IEventAggregator
    {
        private readonly ISet<IEventBase> events = new HashSet<IEventBase>();

        public TEvent GetEvent<TEvent>() where TEvent : IEventBase, new()
        {
            foreach (var @event in events)
            {
                if (@event is TEvent)
                    return (TEvent) @event;
            }
            var e = new TEvent();
            events.Add(e);
            return e;
        }
    }
}
