using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using Prism.Commands;

namespace DataHelpers.App.Shell.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Data Helpers App";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        private void Navigate(string navigationPath)
        {
            RegionManager.RequestNavigate(RegionNames.MainRegion, navigationPath);
        }
    }
}
