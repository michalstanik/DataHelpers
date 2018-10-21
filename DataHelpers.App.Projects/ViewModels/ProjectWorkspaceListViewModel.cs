using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Projects.Wrapper;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Projects;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectWorkspaceListViewModel : DetailViewModelBase, IProjectWorkspaceListViewModel
    {
        private readonly IProjectRepository _projectRepository;
        private ProjectWrapper _project;
        private ProjectWorkspaceWrapper _projectWorkspace;

        public ProjectWorkspaceListViewModel(IMessageDialogService messageDialogService,
            IProjectRepository projectRepository)
            :base(messageDialogService)
        {
            _projectRepository = projectRepository;
            ProjectWorkspace = new ObservableCollection<ProjectWorkspaceWrapper>();
        }

        public async override Task LoadAsync(int? projectId)
        {
            var project = projectId.HasValue
                ? await _projectRepository.GetByIdAsync(projectId.Value)
                : throw new NullReferenceException();

            Id = project.Id;
            Project = new ProjectWrapper(project);

            var projectWorkspace = await _projectRepository.GetProjectWorkspacesForProject(Id);
            ProjectWorkspace.Clear();
            foreach (var item in projectWorkspace)
            {
                var wrapper  = new ProjectWorkspaceWrapper(item);
                ProjectWorkspace.Add(wrapper);
            }
            

            SetTitle();
             await Task.Delay(0);
        }

        public ProjectWrapper Project
        {
            get { return _project; }
            private set
            {
                _project = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProjectWorkspaceWrapper> ProjectWorkspace { get; }

        private void SetTitle()
        {
            Title = $"Workspace: {Project.ProjectName}";
        }

        protected override void OnDeleteExecute()
        {
            
        }

        protected override bool OnSaveCanExecute()
        {
            return true;
        }

        protected override void OnSaveExecute()
        {
            
        }
    }
}
