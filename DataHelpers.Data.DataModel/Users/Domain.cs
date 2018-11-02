using System.ComponentModel.DataAnnotations;

namespace DataHelpers.Data.DataModel.Users
{
    public class Domain : DBEntity
    {
        public int Id { get; set; }

        [Required]
        public string DomainName { get; set; }
    }
}