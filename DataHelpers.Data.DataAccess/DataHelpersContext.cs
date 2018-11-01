using DataHelpers.Data.DataModel.Projects;
using System;
using System.Data.Entity;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override Task<int> SaveChangesAsync()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreationTime").CurrentValue = DateTime.Now;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedTime").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync();
        }
    }
}
