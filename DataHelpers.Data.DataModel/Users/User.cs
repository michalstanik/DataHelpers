namespace DataHelpers.Data.DataModel.Users
{
    public class User : DBEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public bool IsDisabled { get; set; }

        public int? DomainId { get; set; }
        public Domain Domain { get; set; }
    }
}
