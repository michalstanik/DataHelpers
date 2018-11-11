using System;
using System.Threading.Tasks;
using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Projects.Wrapper;
using DataHelpers.Data.DataAccess.Interfaces;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectFilesListViewModel : DetailViewModelBase, IProjectFilesListViewModel
    {
        private IProjectRepository _projectRepository;
        private ProjectWrapper _project;

        public ProjectFilesListViewModel(
           IMessageDialogService messageDialogService,
           IProjectRepository projectRepository
            ) : base(messageDialogService)
        {
            _projectRepository = projectRepository;
        }

        public async override Task LoadAsync(int? projectId)
        {
            var project = projectId.HasValue
                ? await _projectRepository.GetByIdAsync(projectId.Value)
                : throw new NullReferenceException();

            Id = project.Id;
            Project = new ProjectWrapper(project);
            SetTitle();
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

        private void SetTitle()
        {
            Title = $"Project Files: {Project.ProjectName}";
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
