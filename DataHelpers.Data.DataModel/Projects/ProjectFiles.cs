namespace DataHelpers.Data.DataModel.Projects
{
    public class ProjectFiles : DBEntity
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string XMLContent { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
