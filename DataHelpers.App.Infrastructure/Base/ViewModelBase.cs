using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;

namespace DataHelpers.App.Infrastructure.Base
{
    public class ViewModelBase : BindableBase, INavigationAware, INotifyPropertyChanged
    {
        private IUnityContainer unityContainer;
        private IRegionManager regionManager;
        private IEventAggregator eventAggregator;

        public ViewModelBase()
        {
            this.Container = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUnityContainer>();
            this.RegionManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IRegionManager>();
            this.EventAggregator = CommonServiceLocator.ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public IUnityContainer Container
        {
            get { return unityContainer; }
            private set { this.SetProperty<IUnityContainer>(ref this.unityContainer, value); }
        }

        public IRegionManager RegionManager
        {
            get { return regionManager; }
            private set { this.SetProperty<IRegionManager>(ref this.regionManager, value); }
        }

        public IEventAggregator EventAggregator
        {
            get { return eventAggregator; }
            private set { this.SetProperty<IEventAggregator>(ref this.eventAggregator, value); }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        protected new virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object ViewLoaded
        {
            get
            {
                OnViewLoaded();
                return null;
            }
        }

        protected virtual void OnViewLoaded() { }

    }
}
