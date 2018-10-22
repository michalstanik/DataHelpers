using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Infrastructure.ViewModels;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Projects;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectNavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private NavigationDirectoryItemViewModel _rootDirectoryItem;
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

                if (relatedEntities != null)
                {
                    var groupItemWorkspace = new NavigationDirectoryItemViewModel
                        (item.Id, TreeViewNames.Workspaces,
                        nameof(ProjectWorkspaceListViewModel),
                        IconNames.EmoticonCool);

                    tempProject.AddDirItem(groupItemWorkspace);

                    var groupItemComponents = new NavigationDirectoryItemViewModel
                        (item.Id, TreeViewNames.Components,
                        nameof(ProjectComponentsListViewModel));

                    tempProject.AddDirItem(groupItemComponents);

                    foreach (var relatedEntitiesItem in relatedEntities)
                    {
                        switch (relatedEntitiesItem.Entity)
                        {
                            case nameof(ProjectWorkspace):

                                if (relatedEntitiesItem.DisplayMember == null)
                                {
                                    relatedEntitiesItem.DisplayMember = "***no name***";
                                }

                                groupItemWorkspace.AddDirItem(new NavigationDirectoryItemViewModel
                                    (relatedEntitiesItem.Id,
                                    relatedEntitiesItem.DisplayMember,
                                    nameof(ProjectWorkspaceViewModel)));

                                break;

                            case nameof(ProjectComponent):
                                groupItemComponents.AddDirItem(new NavigationDirectoryItemViewModel
                                    (relatedEntitiesItem.Id,
                                    relatedEntitiesItem.DisplayMember,
                                    nameof(ProjectComponentViewModel)));
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
