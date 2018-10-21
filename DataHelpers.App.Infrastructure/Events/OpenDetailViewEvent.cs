using Prism.Events;

namespace DataHelpers.App.Infrastructure.Events
{
    public class OpenDetailViewEvent : PubSubEvent<OpenDetailViewEventArgs>
    {

    }

    public class OpenDetailViewEventArgs
    {
        public int? Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
