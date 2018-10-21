using DataHelpers.App.Infrastructure.Base;

namespace DataHelpers.App.Infrastructure.ViewModels
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private string _image;
        private string _detailViewModelName;

        public NavigationItemViewModel(int id, 
            string displayMember, 
            string detailViewModelName,
            string image = null
            )
        {
            Id = id;
            DisplayMember = displayMember;
            Image = image;
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
