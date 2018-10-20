using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Infrastructure.ViewModels;
using DataHelpers.App.Projects.Services;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Projects;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectNavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private  NavigationDirectoryItemViewModel _rootDirectoryItem;
        private readonly IProjectLookupDataService _projectLookupService;
        private List<NavigationItemViewModel> _dirItems;

        public ProjectNavigationViewModel(IProjectLookupDataService projectLookupService)
        {
            _projectLookupService = projectLookupService;
            Projects = new ObservableCollection<NavigationItemViewModel>();
            //var itemProvider = new ItemProvider();
            _rootDirectoryItem = new NavigationDirectoryItemViewModel(0, "Root", "Root");
            //DirItems = itemProvider.DirItems;
            
        }
        public async Task LoadAsync()
        {
            var childList = new List<NavigationDirectoryItemViewModel>();
            var proejctlookup = await _projectLookupService.GetProjectLookupAsync();

            //Projects.Clear();

            foreach (var item in proejctlookup)
            {
                var tempProject = new NavigationDirectoryItemViewModel(item.Id, item.DisplayMember,
                    nameof(ProjectDetailViewModel));
                childList.Add(tempProject);

                var relatedEntities = _projectLookupService.GetRelatedEntites(item.Id);

                if(relatedEntities != null)
                {
                    foreach (var relatedEntitiesItem in relatedEntities)
                    {
                        switch (relatedEntitiesItem.Entity)
                        {
                            case nameof(ProjectWorkspace):
                                tempProject.AddDirItem(new NavigationDirectoryItemViewModel
                                    (relatedEntitiesItem.Id,
                                    relatedEntitiesItem.DisplayMember,
                                    nameof(ProjectWorkspaceViewModel)));
                                break;

                            default:
                                break;
                        }
                    }
                }



            }
            _rootDirectoryItem.Items = childList;
            DirItems = _rootDirectoryItem.Traverse(_rootDirectoryItem);
        }

        public ObservableCollection<NavigationItemViewModel> Projects { get; }

        public List<NavigationItemViewModel> DirItems
        {
            get { return _dirItems; }
            set
            {
                _dirItems = value;
                OnPropertyChanged(nameof(DirItems));
            }
        }
    }
}
