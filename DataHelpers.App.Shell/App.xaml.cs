﻿using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.App.Projects;
using DataHelpers.App.Shell.Services;
using DataHelpers.App.Shell.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
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

            }

            App.Current.MainWindow.Show();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
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
    }
}
