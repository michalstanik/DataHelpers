using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using Prism.Regions;
using System.Windows;

namespace DataHelpers.App.Shell.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindowViewBase
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            if (regionManager != null)
            {
                SetRegionManager(regionManager, this.flyoutsControlRegion, RegionNames.FlyoutRegion);
            }
        }

        void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
    }
}
