using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class SalesLead
    {
        public SalesLead()
        {
            this.SalesLeadFollowUpRecords = new List<SalesLeadFollowUpRecord>();
            this.SalesLeadItemValues = new List<SalesLeadItemValue>();
            this.SalesLeadOperatingRecords = new List<SalesLeadOperatingRecord>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public string CompanyName { get; set; }
        public string DptName { get; set; }
        public string JobTitle { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WeChatId { get; set; }
        public string QQ { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public System.Guid OwnerEmpGuid { get; set; }
        public int DptId { get; set; }
        public string Description { get; set; }
        public Nullable<int> FollowUpStatusValueId { get; set; }
        public Nullable<System.DateTime> FollowUpDateTime { get; set; }
        public int ClueSourceValueId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual Department Department { get; set; }
        public virtual ItemValue ItemValue { get; set; }
        public virtual ItemValue ItemValue1 { get; set; }
        public virtual ICollection<SalesLeadFollowUpRecord> SalesLeadFollowUpRecords { get; set; }
        public virtual ICollection<SalesLeadItemValue> SalesLeadItemValues { get; set; }
        public virtual ICollection<SalesLeadOperatingRecord> SalesLeadOperatingRecords { get; set; }
    }
}
