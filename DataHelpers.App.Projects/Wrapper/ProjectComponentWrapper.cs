using DataHelpers.App.Infrastructure.Wrapper;
using DataHelpers.Data.DataModel.Projects;

namespace DataHelpers.App.Projects.Wrapper
{
    public class ProjectComponentWrapper : ModelWrapper<ProjectComponent>
    {
        public ProjectComponentWrapper(ProjectComponent model) : base(model)
        {

        }

        public string ComponentName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
