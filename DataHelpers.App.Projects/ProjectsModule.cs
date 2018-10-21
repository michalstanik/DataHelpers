using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Projects.ViewModels;
using DataHelpers.App.Projects.Views;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataAccess.Lookups;
using DataHelpers.Data.DataAccess.Repository;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace DataHelpers.App.Projects
{
    public class ProjectsModule : IModule
    {
        private readonly IUnityContainer _container;

        public ProjectsModule(IUnityContainer container)
        {
            _container = container;
        }


        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IProjectRepository, ProjectRepository>();
            containerRegistry.Register<IProjectLookupDataService, LookupDataService>();
            containerRegistry.Register<IProjectTypeLookupDataService, LookupDataService>();
            containerRegistry.RegisterInstance<INavigationViewModel>(_container.Resolve<ProjectNavigationViewModel>());

            containerRegistry.Register<IProjectDetailViewModel, ProjectDetailViewModel>();
            containerRegistry.Register<IProjectWorkspaceListViewModel, ProjectWorkspaceListViewModel>();
            containerRegistry.Register<object, ProjectsView>("ProjectsView");
        }

    }
}