using DataHelpers.App.Infrastructure.Commands;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows.Input;

namespace DataHelpers.App.Infrastructure.Services
{
    public class FlyoutService : IFlyoutService
    {
        IRegionManager _regionManager;

        public ICommand ShowFlyoutCommand { get; private set; }

        public FlyoutService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;
            ShowFlyoutCommand = new DelegateCommand<FlayoutParamaters>(ShowFlyout, CanShowFlyout);
            applicationCommands.ShowFlyoutCommand.RegisterCommand(ShowFlyoutCommand);
        }

        public bool CanShowFlyout(FlayoutParamaters flyoutEntity)
        {
            return true;
        }

        public void ShowFlyout(FlayoutParamaters flyoutEntity)
        {
            var region = _regionManager.Regions[RegionNames.FlyoutRegion];

            if (region != null)
            {
                var flyout = region.Views.Where(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(flyoutEntity.FlyoutName)).FirstOrDefault() as Flyout;

                if (flyout != null)
                {
                    flyout.IsOpen = true;
                }
            }
        }

        public object SetDataContext(FlayoutParamaters parameters, object dataContext)
        {
            var region = _regionManager.Regions[RegionNames.FlyoutRegion];
            var flyout = region.Views.Where(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(parameters.FlyoutName)).FirstOrDefault() as Flyout;
            flyout.DataContext = dataContext;
            return dataContext;
        }
    }
}
