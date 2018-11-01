using System;

namespace DataHelpers.Data.DataModel
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string FieldName { get; set; }
        public DateTime ChangeTime { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }

    }
}
