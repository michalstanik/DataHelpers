using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DataHelpers.Data.DataAccess.Repository
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
               where TEntity : class
               where TContext : DbContext
    {
        protected readonly TContext Context;

        protected GenericRepository(TContext context)
        {
            this.Context = context;
        }

        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }

        public async Task SaveAsync()
        {
            var AddedEntities = Context.ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreationTime").CurrentValue = DateTime.Now;
            });

            var EditedEntities = Context.ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                if (IsAuditableEntity(E))
                    CreateAuditLogEntry(E);

                E.Property("ModifiedTime").CurrentValue = DateTime.Now;
            });
            await Context.SaveChangesAsync();
        }

        private void CreateAuditLogEntry(DbEntityEntry entity)
        {
            var type = entity.Entity.GetType();
            foreach (var propertyName in entity.OriginalValues.PropertyNames)
            {
                var originalValue = "Empty";
                var newValue = "Empty";
                try
                {
                    originalValue = entity.GetDatabaseValues().GetValue<object>(propertyName).ToString();
                }
                catch { }
                try
                {
                    newValue = entity.CurrentValues.GetValue<object>(propertyName).ToString();
                }
                catch { }

                if (originalValue != newValue)
                {
                    var auditEntry = new AuditLog()
                    {
                        EntityName = entity.Entity.GetType().Name,
                        EntityNamespace = entity.Entity.GetType().Namespace,
                        ChangeTime = DateTime.Now,
                        FieldName = propertyName,
                        NewValue = newValue,
                        PreviousValue = originalValue,
                        EntityId = (int)entity.CurrentValues.GetValue<object>("Id")

                    };
                    Context.Set<AuditLog>().Add(auditEntry);
                }
            }
        }

        //TODO: Check only Auditable properties
        private bool IsAuditableEntity(DbEntityEntry entity)
        {
            return true;
        }
    }
}
