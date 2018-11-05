using DataHelpers.Data.DataModel.Users;

namespace DataHelpers.App.Infrastructure.Interfaces
{
    public interface IAuthenticationService
    {
        UserAccount AuthenticateUser(string username, string clearTextPassword);
    }
}
