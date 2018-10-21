using Prism.Events;

namespace DataHelpers.App.Infrastructure.Events
{
    public class AfterDetailDeletedEvent : PubSubEvent<AfterDetailDeletedEventArgs>
    {

    }

    public class AfterDetailDeletedEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
