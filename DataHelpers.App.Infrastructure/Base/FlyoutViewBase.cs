using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Data;

namespace DataHelpers.App.Infrastructure.Base
{
    public class FlyoutViewBase : Flyout
    {
        public FlyoutViewBase()
        {
            // Programmatically bind the view-model's ViewLoaded property to the view's ViewLoaded property.
            BindingOperations.SetBinding(this, ViewLoadedProperty, new Binding("ViewLoaded"));

            //DataContextChanged += OnDataContextChanged;
        }

        public static readonly DependencyProperty ViewLoadedProperty =
            DependencyProperty.Register("ViewLoaded", typeof(object), typeof(FlyoutViewBase),
            new PropertyMetadata(null));
    }
}
