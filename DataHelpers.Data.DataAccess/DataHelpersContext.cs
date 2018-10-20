using DataHelpers.Data.DataModel.Projects;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataHelpers.Data.DataAccess
{
    public class DataHelpersContext : DbContext
    {
        public DataHelpersContext() : base("DHDB")
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<ProjectComponent> ProjectComponents { get; set; }
        public DbSet<ProjectWorkspace> ProjectWorkspaces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
