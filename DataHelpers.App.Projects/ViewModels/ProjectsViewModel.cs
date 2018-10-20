using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        public ProjectsViewModel(INavigationViewModel navigationModel)
        {
            ProjectNavigationViewModel = navigationModel;
        }

        public INavigationViewModel ProjectNavigationViewModel { get; }

        protected override void OnViewLoaded()
        {
            ProjectNavigationViewModel.LoadAsync();
        }
    }
}
