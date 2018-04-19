using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class FollowUpRecord
    {
        public FollowUpRecord()
        {
            this.CustomerFollowUpRecords = new List<CustomerFollowUpRecord>();
            this.FollowUpRecordAttachments = new List<FollowUpRecordAttachment>();
            this.FollowUpRecordEmps = new List<FollowUpRecordEmp>();
            this.SalesLeadFollowUpRecords = new List<SalesLeadFollowUpRecord>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Content { get; set; }
        public int ApproachItemValueId { get; set; }
        public System.DateTime FollowUpDateTime { get; set; }
        public bool TeamVisibleState { get; set; }
        public System.Guid EmpGuid { get; set; }
        public System.Guid UserGuid { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<CustomerFollowUpRecord> CustomerFollowUpRecords { get; set; }
        public virtual ICollection<FollowUpRecordAttachment> FollowUpRecordAttachments { get; set; }
        public virtual ICollection<FollowUpRecordEmp> FollowUpRecordEmps { get; set; }
        public virtual ItemValue ItemValue { get; set; }
        public virtual ICollection<SalesLeadFollowUpRecord> SalesLeadFollowUpRecords { get; set; }
    }
}
