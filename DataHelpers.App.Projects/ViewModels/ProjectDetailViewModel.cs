using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Projects.Wrapper;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel;
using DataHelpers.Data.DataModel.Projects;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DataHelpers.App.Projects.ViewModels
{
    public class ProjectDetailViewModel : DetailViewModelBase, IProjectDetailViewModel
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTypeLookupDataService _projectTypeLookupDataService;
        private readonly IMessageDialogService _messageDialogService;
        private ProjectWrapper _project;
        private ProjectComponentWrapper _selectedProjectComponent;

        public ProjectDetailViewModel(
            IProjectRepository projectRepository,
            IProjectTypeLookupDataService projectTypeLookupDataService,
            IMessageDialogService messageDialogService)
            : base(messageDialogService)
        {
            _projectRepository = projectRepository;
            _projectTypeLookupDataService = projectTypeLookupDataService;
            _messageDialogService = messageDialogService;

            RemoveComponentCommand = new DelegateCommand(OnRemoveComponentExecute, OnRemoveComponentCanExecute);
            AddComponentCommand = new DelegateCommand(OnAddComponentExecute);

            ProjectTypes = new ObservableCollection<LookupItem>();
            ProjectComponents = new ObservableCollection<ProjectComponentWrapper>();
        }

        public override async Task LoadAsync(int? projectId)
        {
            var project = projectId.HasValue
                ? await _projectRepository.GetByIdAsync(projectId.Value)
                : CreateNewProject();

            Id = project.Id;

            InitializeProject(project);
            InitializeProjectComponents(project.ProjectComponents);

            await LoadProjectTypesLookupAsync();
        }

        protected override async void OnDeleteExecute()
        {
            var result = await _messageDialogService.ShowOkCancelDialogAsync($"Do you really want to delete the project {Project.ProjectName}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _projectRepository.Remove(Project.Model);
                await _projectRepository.SaveAsync();
                RaiseDetailDeletedEvent(Project.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Project != null
                && !Project.HasErrors
                && ProjectComponents.All(pc => !pc.HasErrors)
                && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _projectRepository.SaveAsync();
            HasChanges = _projectRepository.HasChanges();
            Id = Project.Id;
            RaiseDetailSavedEvent(Project.Id, Project.ProjectName);
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

        public ProjectComponentWrapper SelectedProjectComponent
        {
            get { return _selectedProjectComponent; }
            set
            {
                _selectedProjectComponent = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveComponentCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<LookupItem> ProjectTypes { get; }
        public ObservableCollection<ProjectComponentWrapper> ProjectComponents { get; }

        public ICommand RemoveComponentCommand { get; }
        public ICommand AddComponentCommand { get; }
        public ICommand SelectPath { get; }

        private Project CreateNewProject()
        {
            var project = new Project();
            _projectRepository.Add(project);
            return project;
        }

        private async Task LoadProjectTypesLookupAsync()
        {
            ProjectTypes.Clear();
            ProjectTypes.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _projectTypeLookupDataService.GetProjectTypeLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProjectTypes.Add(lookupItem);
            }
        }

        private void InitializeProject(Project project)
        {
            Project = new ProjectWrapper(project);

            Project.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _projectRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Project.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Project.ProjectName))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Project.Id == 0)
            {
                //Trick to trigger the validation
                Project.ProjectName = "";
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Project.ProjectName}";
        }

        private void InitializeProjectComponents(ICollection<ProjectComponent> projectComponents)
        {
            foreach (var wrapper in ProjectComponents)
            {
                wrapper.PropertyChanged -= ProjectComponentWrapper_PropertyChanged;
            }
            ProjectComponents.Clear();
            foreach (var projectComponent in projectComponents)
            {
                var wrapper = new ProjectComponentWrapper(projectComponent);
                ProjectComponents.Add(wrapper);
                wrapper.PropertyChanged += ProjectComponentWrapper_PropertyChanged;
            }
        }

        private void ProjectComponentWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _projectRepository.HasChanges();
            }
            if (e.PropertyName == nameof(ProjectComponentWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private bool OnRemoveComponentCanExecute()
        {
            return SelectedProjectComponent != null;
        }

        private void OnRemoveComponentExecute()
        {
            SelectedProjectComponent.PropertyChanged -= ProjectComponentWrapper_PropertyChanged;
            _projectRepository.RemoveProjectComponent(SelectedProjectComponent.Model);
            ProjectComponents.Remove(SelectedProjectComponent);
            SelectedProjectComponent = null;
            HasChanges = _projectRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddComponentExecute()
        {
            var newComponent = new ProjectComponentWrapper(new ProjectComponent());
            newComponent.PropertyChanged += ProjectComponentWrapper_PropertyChanged;
            ProjectComponents.Add(newComponent);
            Project.Model.ProjectComponents.Add(newComponent.Model);
            newComponent.ComponentName = ""; // Trigger validation :-)
        }
    }
}
