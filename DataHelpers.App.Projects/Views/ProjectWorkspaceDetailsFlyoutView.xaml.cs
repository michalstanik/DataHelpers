using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;

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

        public int? EntityId { get; set; }
    }
}
