using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataHelpers.App.Infrastructure.Base
{
    public class UserControlViewBase : UserControl
    {
        public UserControlViewBase()
        {
            // Programmatically bind the view-model's ViewLoaded property to the view's ViewLoaded property.
            BindingOperations.SetBinding(this, ViewLoadedProperty, new Binding("ViewLoaded"));
        }

        public static readonly DependencyProperty ViewLoadedProperty =
            DependencyProperty.Register("ViewLoaded", typeof(object), typeof(UserControlViewBase),
            new PropertyMetadata(null));
    }
}
