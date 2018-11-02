using DataHelpers.Data.DataModel;
using DataHelpers.Data.DataModel.Projects;
using DataHelpers.Data.DataModel.Users;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Domain> Doamains { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
