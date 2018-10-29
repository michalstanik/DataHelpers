using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Projects.Wrapper;
using DataHelpers.Data.DataAccess.Interfaces;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectWorkspaceDetailsFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectRepository _projectRepository;
        private ProjectWorkspaceWrapper _projectWorkspace;

        public ProjectWorkspaceDetailsFlyoutViewModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public int EntityId { get; set; }

        protected override void OnViewLoaded()
        {
            if (EntityId != 0)
            {
                //TODO: Load an Entity
                var workspace = _projectRepository.GetProjectWorkspace(EntityId);
                ProjectWorkspace = new ProjectWorkspaceWrapper(workspace);
            }
        }

        public ProjectWorkspaceWrapper ProjectWorkspace
        {
            get { return _projectWorkspace; }
            private set
            {
                _projectWorkspace = value;
                OnPropertyChanged();
            }
        }
    }
}
