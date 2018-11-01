namespace DataHelpers.Data.DataModel.Projects
{
    public class ProjectWorkspace : DBEntity
    {
        public int Id { get; set; }

        public string WorkspaceName { get; set; }

        public string WorkspacePath { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}