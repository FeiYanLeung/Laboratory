using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class SalesLeadFollowUpRecord
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int SalesLoadId { get; set; }
        public int FollowUpRecordId { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual FollowUpRecord FollowUpRecord { get; set; }
        public virtual SalesLead SalesLead { get; set; }
    }
}
