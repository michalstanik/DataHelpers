using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.App.Infrastructure.Interfaces;

namespace DataHelpers.App.Shell.Views
{
    /// <summary>
    /// Interaction logic for UserAuthenticationFlyoutView.xaml
    /// </summary>
    public partial class UserAuthenticationFlyoutView : FlyoutViewBase, IFlyoutView
    {
        public UserAuthenticationFlyoutView()
        {
            InitializeComponent();
        }

        public string FlyoutName
        {
            get { return FlyoutNames.UserAuthenticationFlyoutView; }
        }

        public int? EntityId { get; set; }
    }
}
