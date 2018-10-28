using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataHelpers.App.Projects.Views
{
    /// <summary>
    /// Interaction logic for ProjectWorkspaceDetailsFlyoutView.xaml
    /// </summary>
    public partial class ProjectWorkspaceDetailsFlyoutView : FlyoutViewBase, IFlyoutView
    {
        public ProjectWorkspaceDetailsFlyoutView()
        {
            InitializeComponent();
        }

        public string FlyoutName
        {
            get { return FlyoutNames.ProjectWorkspaceDetailsFlyoutView; }
        }
    }
}
