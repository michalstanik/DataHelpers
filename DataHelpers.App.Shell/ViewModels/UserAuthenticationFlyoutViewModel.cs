using DataHelpers.App.Infrastructure.Base;
using DataHelpers.Data.DataAccess.Interfaces;
using System.Threading;

namespace DataHelpers.App.Shell.ViewModels
{
    public class UserAuthenticationFlyoutViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationFlyoutViewModel(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }
        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            var user = Thread.CurrentPrincipal.Identity.Name;
            var appUser = _userRepository.GetUserByName(user);

            //TODO: FlipView

            if (appUser != null)
            {
                //If user is known, check if account exist
                //Let im LogIn automatically - and hide slider and update header
            }
            else
            {
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
