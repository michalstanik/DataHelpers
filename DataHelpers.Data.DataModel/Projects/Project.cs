using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DataHelpers.Data.DataModel.Projects
{
    public class Project
    {
        public Project()
        {
            ProjectComponents = new Collection<ProjectComponent>();
            ProjectWorkspaces = new Collection<ProjectWorkspace>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public int? ProjectTypeId { get; set; }

        public ProjectType ProjectType { get; set; }

        public ICollection<ProjectComponent> ProjectComponents { get; set; }
        public ICollection<ProjectWorkspace> ProjectWorkspaces { get; set; }

    }
}
