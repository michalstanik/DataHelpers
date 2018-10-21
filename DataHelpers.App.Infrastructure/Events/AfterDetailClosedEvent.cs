using Prism.Events;

namespace DataHelpers.App.Infrastructure.Events
{
    public class AfterDetailClosedEvent : PubSubEvent<AfterDetailClosedEventArgs>
    {
    }
    public class AfterDetailClosedEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
