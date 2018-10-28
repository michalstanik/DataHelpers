namespace DataHelpers.App.Infrastructure.Interfaces
{
    public interface IFlyoutView
    {
        string FlyoutName { get; }
        int? EntityId { get; set; }
    }
}
