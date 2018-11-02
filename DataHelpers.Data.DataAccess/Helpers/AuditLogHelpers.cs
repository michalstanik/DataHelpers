using System.Data.Entity.Infrastructure;

namespace DataHelpers.Data.DataAccess.Helpers
{
    public static class AuditLogHelpers
    {
        public static bool IsAuditableField(DbEntityEntry entity, string propertyName)
        {
            var entityType = entity.Entity.GetType();
            var fieldsWithAttribiute = entityType.GetProperty(propertyName);
            if (fieldsWithAttribiute != null)
            {
                foreach (var attribute in fieldsWithAttribiute.GetCustomAttributes(true))
                {
                    if (attribute.ToString() == "DataHelpers.Data.DataModel.Helpers.Auditable") return true;
                }
            }
            return false;
        }

        public static bool IsAuditableEntity(DbEntityEntry entity)
        {
            var entityType = entity.Entity.GetType();

            foreach (var attribute in entityType.GetCustomAttributes(true))
            {
                if (attribute.ToString() == "DataHelpers.Data.DataModel.Helpers.Auditable") return true;
            }
            return false;
        }
    }
}
