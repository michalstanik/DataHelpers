using System.ComponentModel.DataAnnotations;

namespace DataHelpers.Data.DataModel.Projects
{
    public class ProjectType : DBEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectTypeName { get; set; }
    }
}