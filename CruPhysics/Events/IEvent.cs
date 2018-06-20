using System;

namespace CruPhysics.Events
{
    public interface IEventBase
    {

    }

    public interface IEvent<TPayload> : IEventBase
    {
        void Subscribe(Action<TPayload> action);
        void Unsubscribe(Action<TPayload> action);
        void Publish(TPayload payload);
    }

    public class Event<TPayload> : IEvent<TPayload>
    {
        Action<TPayload> subscribers;

        public void Publish(TPayload payload)
        {
            subscribers?.Invoke(payload);
        }

        public void Subscribe(Action<TPayload> action)
        {
            subscribers += action;
        }

        public void Unsubscribe(Action<TPayload> action)
        {
            subscribers -= action;
        }
    }
}
