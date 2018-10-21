using System.Collections.Generic;

namespace DataHelpers.Data.DataModel
{
    public class LookupItem
    {
        public int Id { get; set; }

        public string DisplayMember { get; set; }

        public string Entity { get; set; }

        public IEnumerable<LookupItem> RelatedEntities { get; set; }

        public string Image { get; set; }
    }



    public class NullLookupItem : LookupItem
    {
        public new int? Id { get { return null; } }
    }
}
