using DataHelpers.App.Infrastructure.Services;

namespace DataHelpers.App.Infrastructure.Interfaces
{
    public interface IFlyoutService
    {
        void ShowFlyout(FlayoutParamaters flyoutEntity);

        bool CanShowFlyout(FlayoutParamaters flyoutEntity);
    }
}
