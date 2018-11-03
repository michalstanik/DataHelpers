using DataHelpers.Data.DataModel.Users;

namespace DataHelpers.Data.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserByName(string userName);
        User GetUserByName(string userName, string domain);
    }
}
