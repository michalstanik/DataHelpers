using DataHelpers.Data.DataAccess.Interfaces;
using DataHelpers.Data.DataModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DataHelpers.Data.DataAccess.Helpers;
using DataHelpers.App.Infrastructure.Constants;

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

                if (originalValue != newValue && IsAuditableField(entity, propertyName))
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
        public static bool IsAuditableField(DbEntityEntry entity, string propertyName)
        {
            var entityType = entity.Entity.GetType();
            var fieldsWithAttribiute = entityType.GetProperty(propertyName);
            if (fieldsWithAttribiute != null)
            {
                foreach (var attribute in fieldsWithAttribiute.GetCustomAttributes(true))
                {
                    if (attribute.ToString() == AuditAttributeNames.Auditable) return true;
                }
            }
            return false;
        }

        public static bool IsAuditableEntity(DbEntityEntry entity)
        {
            var entityType = entity.Entity.GetType();

            foreach (var attribute in entityType.GetCustomAttributes(true))
            {
                if (attribute.ToString() == AuditAttributeNames.Auditable) return true;
            }
            return false;
        }
    }
}
