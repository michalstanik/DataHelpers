using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Events;
using DataHelpers.App.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly Func<IProjectDetailViewModel> _projectDetailViewModelCreator;
        private readonly Func<IProjectWorkspaceListViewModel> _projectWokspaceListViewModelCreator;
        private IDetailViewModel _selectedDetailViewModel;

        public ProjectsViewModel(INavigationViewModel navigationModel,
            Func<IProjectDetailViewModel> projectDetailViewModelCreator,
            Func<IProjectWorkspaceListViewModel> projectWokspaceListViewModelCreator)
        {
            _projectDetailViewModelCreator = projectDetailViewModelCreator;
            _projectWokspaceListViewModelCreator = projectWokspaceListViewModelCreator;
            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            EventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            EventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);

            ProjectNavigationViewModel = navigationModel;
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                   .SingleOrDefault(vm => vm.Id == id
                   && vm.GetType().Name == viewModelName);
            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == args.Id
                && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                switch (args.ViewModelName)
                {
                    case nameof(ProjectDetailViewModel):
                        detailViewModel = _projectDetailViewModelCreator();
                        break;
                    case nameof(ProjectWorkspaceListViewModel):
                        detailViewModel = _projectWokspaceListViewModelCreator();
                        break;
                    default:
                        break;
                }
                await detailViewModel.LoadAsync(args.Id);
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
        }

        public INavigationViewModel ProjectNavigationViewModel { get; }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        protected override void OnViewLoaded()
        {
            ProjectNavigationViewModel.LoadAsync();
        }
    }
}
