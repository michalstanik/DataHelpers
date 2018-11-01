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

        public ProjectWorkspace GetProjectWorkspace(int workspaceId)
        {
            return Context.ProjectWorkspaces.Where(w => w.Id == workspaceId).FirstOrDefault();
        }

        public IEnumerable<ProjectWorkspace> GetProjectWorkspacesForProject(int projectId)
        {
            return Context.ProjectWorkspaces.Where(s => s.ProjectId == projectId);
        }

        public void RemoveProjectComponent(ProjectComponent model)
        {
            Context.ProjectComponents.Remove(model);
        }

        public void RemoveProjectWorkspace(ProjectWorkspace model)
        {
            Context.ProjectWorkspaces.Remove(model);
        }
    }
}
