using System.Collections.Generic;
using System.Threading.Tasks;
using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Infrastructure.ViewModels;
using DataHelpers.App.Projects.Services;
using DataHelpers.Data.DataAccess.Interfaces;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectNavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IProjectLookupDataService _projectLookupService;
        private List<NavigationItemViewModel> _dirItems;

        public ProjectNavigationViewModel(IProjectLookupDataService projectLookupService)
        {
            _projectLookupService = projectLookupService;
            var itemProvider = new ItemProvider();
            DirItems = itemProvider.DirItems;
        }
        public async Task LoadAsync()
        {
            var lookup = await _projectLookupService.GetProjectLookupAsync();
        }

        public List<NavigationItemViewModel> DirItems
        {
            get { return _dirItems; }
            set
            {
                _dirItems = value;
                //OnPropertyChanged(nameof(DirItems));
            }
        }
    }
}
