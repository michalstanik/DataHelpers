using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel;
using DataHelpers.Data.DataModel.Projects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataHelpers.Data.DataAccess.Lookups
{
    public class LookupDataService : IProjectLookupDataService, IProjectTypeLookupDataService
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
                        DisplayMember = f.ProjectName,
                        Entity = nameof(Project)

                    })
                    .ToListAsync();
            }
        }

        public IEnumerable<LookupItem> GetRelatedEntites(int projectId)
        {
            var relatedEntites = new List<LookupItem>();
            using (var ctx = _contextCreator())
            {
                var projectWorskpaces = ctx.ProjectWorkspaces.Where(p => p.ProjectId == projectId).ToList();

                if ( projectWorskpaces.Count() != 0)
                {
                    foreach (var item in projectWorskpaces)
                    {
                        relatedEntites.Add(new LookupItem
                        {
                            Id = item.Id,
                            DisplayMember = item.WorkspaceName,
                            Entity = nameof(ProjectWorkspace),
                            RelatedEntities = null
                        });
                    }
                }

                var projectComponents = ctx.ProjectComponents.Where(p => p.ProjectId == projectId).ToList();

                if (projectComponents.Count() != 0)
                {
                    foreach (var item in projectComponents)
                    {
                        relatedEntites.Add(new LookupItem
                        {
                            Id = item.Id,
                            DisplayMember = item.ComponentName,
                            Entity = nameof(ProjectComponent),
                            RelatedEntities = null
                        });
                    }
                }

            }

            return  relatedEntites;
        }

        public async Task<IEnumerable<LookupItem>> GetProjectTypeLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProjectTypes.AsNoTracking()
                    .Select(f =>
                    new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.ProjectTypeName
                    })
                    .ToListAsync();
            }
        }
    }
}
