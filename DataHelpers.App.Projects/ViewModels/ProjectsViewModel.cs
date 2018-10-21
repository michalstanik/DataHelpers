using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using System;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly Func<IProjectDetailViewModel> _projectDetailViewModelCreator;

        public ProjectsViewModel(INavigationViewModel navigationModel,
            Func<IProjectDetailViewModel> projectDetailViewModelCreator)
        {
            ProjectNavigationViewModel = navigationModel;
            _projectDetailViewModelCreator = projectDetailViewModelCreator;
        }

        public INavigationViewModel ProjectNavigationViewModel { get; }

        protected override void OnViewLoaded()
        {
            ProjectNavigationViewModel.LoadAsync();
        }
    }
}
