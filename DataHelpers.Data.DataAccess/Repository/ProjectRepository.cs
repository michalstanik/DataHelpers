using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel.Projects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataHelpers.Data.DataAccess.Repository
{
    public class ProjectRepository : GenericRepository<Project, DataHelpersContext>, IProjectRepository
    {
        public ProjectRepository(DataHelpersContext context)
               : base(context)
        {
        }

        public override async Task<Project> GetByIdAsync(int projectId)
        {
            return await Context.Projects
                .Include(p => p.ProjectComponents)
                .Include(p => p.ProjectWorkspaces)
                .SingleAsync(p => p.Id == projectId);
        }

        public async Task<IEnumerable<ProjectWorkspace>> GetProjectWorkspacesForProject(int projectId)
        {
            return await Context.ProjectWorkspaces.Where(s => s.ProjectId == projectId).ToListAsync();
        }

        public void RemoveProjectComponent(ProjectComponent model)
        {
            Context.ProjectComponents.Remove(model);
        }
    }
}
