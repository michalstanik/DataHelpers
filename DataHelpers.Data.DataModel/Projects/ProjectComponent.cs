using System.ComponentModel.DataAnnotations;

namespace DataHelpers.Data.DataModel.Projects
{
    public class ProjectComponent : DBEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ComponentName { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}