using DataHelpers.App.Infrastructure.Base;
using DataHelpers.App.Infrastructure.Constants;
using DataHelpers.Data.DataAccess.Interfaces;
using System.Threading;

namespace DataHelpers.App.Shell.ViewModels
{
    public class UserAuthenticationFlyoutViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;
        private string _authenticationStatus;
        private bool _showHideRegistrationForm;
        private bool _showHideLoginForm;

        public UserAuthenticationFlyoutViewModel(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        public string AuthenticationStatus
        {
            get { return _authenticationStatus; }
            private set
            {
                _authenticationStatus = value;
                OnPropertyChanged();
            }
        }

        public bool ShowHideRegistrationForm
        {
            get { return _showHideRegistrationForm; }
            private set
            {
                _showHideRegistrationForm = value;
                OnPropertyChanged();
            }
        }

        public bool ShowHideLoginForm
        {
            get { return _showHideLoginForm; }
            private set
            {
                _showHideLoginForm = value;
                OnPropertyChanged();
            }
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            var user = Thread.CurrentPrincipal.Identity.Name;
            var appUser = _userRepository.GetUserByName(user);

            //TODO: FlipView

            if (appUser != null)
            {
                //Store Information about login
                //If user is known, check if account exist
                //Let im LogIn automatically - and hide slider and update header
            }
            else
            {
                AuthenticationStatus = $"{user}: {UserMessages.AuthUserUnknown}";
                ShowHideRegistrationForm = true;
                ShowHideLoginForm = true;
                //If user is not known
                //Let im associate user with account
                //If account does not exists yet - create new one and assosiate
            }
                       
            //Many to many - account and projects
            //Funcionality of sharing projects with users  

            //Administration role
            //Administration tab for user management 
        }
    }
}
