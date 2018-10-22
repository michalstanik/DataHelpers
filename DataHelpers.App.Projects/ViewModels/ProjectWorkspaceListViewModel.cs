using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Projects.Wrapper;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Projects;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Data.Entity;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectWorkspaceListViewModel : DetailViewModelBase, IProjectWorkspaceListViewModel
    {
        private readonly IMessageDialogService _messageDialogService;
        private readonly IProjectRepository _projectRepository;
        private ProjectWrapper _project;
        private ProjectWorkspaceWrapper _projectWorkspace;

        public ProjectWorkspaceListViewModel(
            IMessageDialogService messageDialogService,
            IProjectRepository projectRepository)
            :base(messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _projectRepository = projectRepository;
            ProjectWorkspace = new ObservableCollection<ProjectWorkspaceWrapper>();

            SelectPath = new DelegateCommand(SelectFileExecute);
        }

        private async void SelectFileExecute()
        {
            FolderBrowserDialog _openFileDialog = new FolderBrowserDialog();

            DialogResult result = _openFileDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(_openFileDialog.SelectedPath))
            {
                //TODO: Lack of exeption handling for Directory.GetFiles
                var files = Directory.GetFiles(_openFileDialog.SelectedPath);

                List<string> extensionsList = new List<string>();

                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file);
                    extensionsList.Add(extension);
                }

                var distinctExtensins = extensionsList.Distinct();

                var dialog = await _messageDialogService
                    .ShowOkCancelDialogAsync($"You selected path {_openFileDialog.SelectedPath}, " +
                    $"which contain {files.Count()} files in {distinctExtensins.Count()} different types. " +
                    $"Do you want to continue with selection?", "Question");

                if (dialog == MessageDialogResult.OK)
                {
                    SavePathInProject(_openFileDialog.SelectedPath);
                }
            }
        }

        private  void SavePathInProject(string selectedPath)
        {
            var existingWorkspacesForProject
                = _projectRepository.GetProjectWorkspacesForProject(Project.Id);

            if (existingWorkspacesForProject.Where(p => p.WorkspacePath == selectedPath).Count() == 0)
            {
                var newWorkspace = new ProjectWorkspaceWrapper(new ProjectWorkspace());
                newWorkspace.WorkspacePath = selectedPath;

                Project.Model.ProjectWorkspaces.Add(newWorkspace.Model);
                //TODO - Integrate with whole model validations
                OnSaveExecute();
            }
            else
            {
                //TODO: Implement info for user, that path already exists
            }
        }

        public async override Task LoadAsync(int? projectId)
        {
            var project = projectId.HasValue
                ? await _projectRepository.GetByIdAsync(projectId.Value)
                : throw new NullReferenceException();

            Id = project.Id;
            Project = new ProjectWrapper(project);

            var projectWorkspace =  _projectRepository.GetProjectWorkspacesForProject(Id);
            ProjectWorkspace.Clear();
            foreach (var item in projectWorkspace)
            {
                var wrapper  = new ProjectWorkspaceWrapper(item);
                ProjectWorkspace.Add(wrapper);
            }
            
            SetTitle();
             await Task.Delay(0);
        }
        #region PublicPropertiesAndCommands

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

        public ICommand SelectPath { get; }

        #endregion

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

        protected async override void OnSaveExecute()
        {
            await _projectRepository.SaveAsync();
            HasChanges = _projectRepository.HasChanges();
            Id = Project.Id;
            RaiseDetailSavedEvent(Project.Id, Project.ProjectName);
        }
    }
}
