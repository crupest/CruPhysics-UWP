namespace CruPhysics.Events
{
    interface IEventAggregator
    {
        TEvent GetEvent<TEvent>() where TEvent : IEventBase, new();
    }
}
