using DataHelpers.Data.DataModel.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataHelpers.Data.DataAccess.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        //Project Component
        void RemoveProjectComponent(ProjectComponent model);

        //Project Workspace 
        IEnumerable<ProjectWorkspace> GetProjectWorkspacesForProject(int Id);
        ProjectWorkspace GetProjectWorkspace(int Id);
    }
}
