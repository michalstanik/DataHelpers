using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataHelpers.Data.DataAccess.Lookups
{
    public class LookupDataService : IProjectLookupDataService
    {
        private Func<DataHelpersContext> _contextCreator;

        public LookupDataService(Func<DataHelpersContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetProjectLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Projects.AsNoTracking()
                    .Select(f =>
                    new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.ProjectName
                    })
                    .ToListAsync();
            }
        }

    }
}
