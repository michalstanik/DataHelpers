using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Commands;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Helpers;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Infrastructure.Services;
using DataHelpers.App.Projects.Wrapper;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Projects;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectWorkspaceListViewModel : DetailViewModelBase, IProjectWorkspaceListViewModel
    {
        private readonly IMessageDialogService _messageDialogService;
        private readonly IProjectRepository _projectRepository;
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommands _applicationCommands;
        private ProjectWrapper _project;
        private ProjectWorkspaceWrapper _selectedProjectWorkspace;

        public ProjectWorkspaceListViewModel(
            IMessageDialogService messageDialogService,
            IProjectRepository projectRepository,
            IRegionManager regionManager, 
            IApplicationCommands applicationCommands)
            :base(messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _projectRepository = projectRepository;
            _regionManager = regionManager;
            _applicationCommands = applicationCommands;
            ProjectWorkspace = new ObservableCollection<ProjectWorkspaceWrapper>();

            SelectPath = new DelegateCommand(SelectFileExecute);
            NewOpenDetailsCommand = new DelegateCommand<int?>(OpenNewWorkspaceFlyout);
        }

        private void OpenNewWorkspaceFlyout(int? id)
        {
            int newId = 0;
            if(id != null) { newId = (int)id; }

            //TODO: Check if open
            var parameters = new FlayoutParamaters
            {
                FlyoutEntityId = newId,
                FlyoutName = FlyoutNames.ProjectWorkspaceDetailsFlyoutView
            };

            var flyoutService = new FlyoutService(_regionManager, _applicationCommands);
            flyoutService.SetDataContext(parameters, new ProjectWorkspaceDetailsFlyoutViewModel(_projectRepository)
            {
                EntityId = parameters.FlyoutEntityId
            });
            flyoutService.ShowFlyout(parameters);
        }

        private async void SelectFileExecute()
        {
            FolderBrowserDialog _openFileDialog = new FolderBrowserDialog()
            {
                Description = UserMessages.FolderBrowserDialogDescription
            };

            DialogResult result = _openFileDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(_openFileDialog.SelectedPath))
            {
                var newHelper = new FileDirectoryHelpers();
                var filesInPath = newHelper.GetFilesInfoInPath(_openFileDialog.SelectedPath);

                var dialog = await _messageDialogService
                    .ShowOkCancelDialogAsync(
                    $"You selected path {_openFileDialog.SelectedPath}, " +
                    $"which contain {filesInPath.FileNumbers} " +
                    $"files in {filesInPath.FilesList.Select(s => s.Extension).Distinct().Count()} " +
                    $"different types. Do you want to continue with selection?", "Question");

                if (dialog == MessageDialogResult.OK)
                {
                    SavePathInProject(_openFileDialog.SelectedPath);
                }
                else
                {
                    //TODO: Recomanded to create new catalog with defoult name 
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

                ProjectWorkspace.Add(newWorkspace);
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

            InitializeProjetWrappers(Id);


            
            SetTitle();
             await Task.Delay(0);
        }

        private void InitializeProjetWrappers(int projectId)
        {
            var projectWorkspace = _projectRepository.GetProjectWorkspacesForProject(projectId);
            foreach (var wrapper in ProjectWorkspace)
            {
                wrapper.PropertyChanged -= ProjectWorkspaceWrapper_PropertyChanged;
            }
            ProjectWorkspace.Clear();
            foreach (var item in projectWorkspace)
            {
                var wrapper = new ProjectWorkspaceWrapper(item);
                ProjectWorkspace.Add(wrapper);
                wrapper.PropertyChanged += ProjectWorkspaceWrapper_PropertyChanged;
            }
        }

        private void ProjectWorkspaceWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _projectRepository.HasChanges();
            }
            if (e.PropertyName == nameof(ProjectWorkspaceWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
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

        public ProjectWorkspaceWrapper SelectedProjectWorkspace
        {
            get { return _selectedProjectWorkspace; }
            set
            {
                _selectedProjectWorkspace = value;
                OnPropertyChanged();
                //((DelegateCommand)RemoveComponentCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ProjectWorkspaceWrapper> ProjectWorkspace { get; }

        public ICommand SelectPath { get; }
        public ICommand NewOpenDetailsCommand { get; set; }
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
            var result =  ProjectWorkspace != null
            && !Project.HasErrors
            && ProjectWorkspace.All(pc => !pc.HasErrors)
            && HasChanges;

            return result;
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
