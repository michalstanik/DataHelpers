using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelpers.Data.DataModel.Users
{
    public class UserAccount : DBEntity
    {
        public UserAccount()
        {
            Users = new Collection<User>();
        }

        public int Id { get; set; }

        public string UserAccountName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
