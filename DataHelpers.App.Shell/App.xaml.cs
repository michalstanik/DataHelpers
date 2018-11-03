using DataHelpers.App.Infrastructure.Commands;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Infrastructure.Services;
using DataHelpers.App.Projects;
using DataHelpers.App.Shell.Services;
using DataHelpers.App.Shell.Views;
using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataAccess.Repository;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Security.Principal;
using System.Windows;

namespace DataHelpers.App.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            var regionManager = this.Container.Resolve<IRegionManager>();
            if (regionManager != null)
            {
                regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(UserAuthenticationFlyoutView));
            }

            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            App.Current.MainWindow.Show();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
            containerRegistry.Register<IApplicationCommands, ApplicationCommandsProxy>();
            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());
            containerRegistry.Register<IUserRepository, UserRepository>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule(typeof(ProjectsModule));
        }

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        //TODO: Integrate with file log 
        //TODO: Log level by UI
        //TODO: View Logs by UI

    }
}
