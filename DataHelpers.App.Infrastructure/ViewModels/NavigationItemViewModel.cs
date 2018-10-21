using DataHelpers.App.Infrastructure.Base;

namespace DataHelpers.App.Infrastructure.ViewModels
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private string _image;
        private string _detailViewModelName;

        public NavigationItemViewModel(int id, string displayMember, string detailViewModelName)
        {
            Id = id;
            DisplayMember = displayMember;
            _detailViewModelName = detailViewModelName;
        }

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
    }
}
