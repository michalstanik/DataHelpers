using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Commands;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Services;
using Prism.Commands;
using Prism.Regions;

namespace DataHelpers.App.Shell.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Data Helpers App";
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommands _applicationCommands;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(
            IRegionManager regionManager,
            IApplicationCommands applicationCommands)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
            _applicationCommands = applicationCommands;
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        private void Navigate(string navigationPath)
        {
            RegionManager.RequestNavigate(RegionNames.MainRegion, navigationPath);
        }

        private void OpenNewUserFlyout()
        {
            var parameters = new FlayoutParamaters
            {
                FlyoutEntityId = 0,
                FlyoutName = FlyoutNames.UserAuthenticationFlyoutView
            };

            var flyoutService = new FlyoutService(_regionManager, _applicationCommands);
            flyoutService.ShowFlyout(parameters);
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            OpenNewUserFlyout();
        }
    }
}
