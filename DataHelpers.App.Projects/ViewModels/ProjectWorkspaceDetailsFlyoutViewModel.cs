using DataHelpers.App.Infrastructure.Base;
using DataHelpers.Data.DataAccess.Interfaces;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectWorkspaceDetailsFlyoutViewModel : ViewModelBase
    {
        private readonly IProjectRepository _projectRepository;

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
            }
        }

    }
}
