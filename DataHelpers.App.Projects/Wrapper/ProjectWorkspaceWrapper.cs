using DataHelpers.App.Infrastructure.Wrapper;
using DataHelpers.Data.DataModel.Projects;

namespace DataHelpers.App.Projects.Wrapper
{
    public class ProjectWorkspaceWrapper : ModelWrapper<ProjectWorkspace>
    {
        public ProjectWorkspaceWrapper(ProjectWorkspace model) : base(model)
        {

        }

        public string WorkspacePath
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int FilesCounter
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
    }
}
