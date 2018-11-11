using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Infrastructure.Services;
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
            var regionManager = _container.Resolve<IRegionManager>();
            if (regionManager != null)
            {
                regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(ProjectWorkspaceDetailsFlyoutView));
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IFlyoutService>(_container.Resolve<FlyoutService>());
            containerRegistry.Register<IProjectRepository, ProjectRepository>();
            containerRegistry.Register<IProjectLookupDataService, LookupDataService>();
            containerRegistry.Register<IProjectTypeLookupDataService, LookupDataService>();
            containerRegistry.RegisterInstance<INavigationViewModel>(_container.Resolve<ProjectNavigationViewModel>());

            containerRegistry.Register<IProjectDetailViewModel, ProjectDetailViewModel>();
            containerRegistry.Register<IProjectWorkspaceListViewModel, ProjectWorkspaceListViewModel>();
            containerRegistry.Register<IProjectFilesListViewModel, ProjectFilesListViewModel>();
            containerRegistry.Register<object, ProjectsView>("ProjectsView");
        }

    }
}