using DataHelpers.Data.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataHelpers.Data.DataAccess.Interfaces
{
    public interface IProjectLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProjectLookupAsync();
    }
}
