using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Users;
using System.Linq;

namespace DataHelpers.Data.DataAccess.Repository
{
    public class UserRepository : GenericRepository<User, DataHelpersContext>, IUserRepository
    {
        public UserRepository(DataHelpersContext context) :base(context)
        {

        }
        public User GetUserByName(string userName, string domain)
        {
            var domainObject = Context.Doamains.Where(d => d.DomainName == domain).SingleOrDefault();

            return Context.Users  
                .Where(p => p.UserName == userName)
                .Where(p => p.Domain == domainObject)
                .SingleOrDefault();
        }
    }
}
