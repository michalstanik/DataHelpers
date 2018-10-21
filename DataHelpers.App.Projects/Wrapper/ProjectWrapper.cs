using DataHelpers.App.Infrastructure.Wrapper;
using DataHelpers.Data.DataModel.Projects;
using System;
using System.Collections.Generic;

namespace DataHelpers.App.Projects.Wrapper
{
    public class ProjectWrapper : ModelWrapper<Project>
    {
        public ProjectWrapper(Project model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string ProjectName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProjectDescription
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int? ProjectTypeId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(ProjectName):
                    if (string.Equals(ProjectName, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Please do not test the test application!";
                    }
                    break;
            }
        }
    }
}
