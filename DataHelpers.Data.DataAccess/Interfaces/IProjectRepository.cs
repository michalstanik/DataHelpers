﻿using DataHelpers.Data.DataModel.Projects;
using System.Collections.Generic;

namespace DataHelpers.Data.DataAccess.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        void RemoveProjectComponent(ProjectComponent model);
        IEnumerable<ProjectWorkspace> GetProjectWorkspacesForProject(int Id);
    }
}
