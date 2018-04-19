using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Customer
    {
        public Customer()
        {
            this.Contacts = new List<Contact>();
            this.CustomerFollowUpRecords = new List<CustomerFollowUpRecord>();
            this.CustomerItemValues = new List<CustomerItemValue>();
            this.CustomerOperatingRecords = new List<CustomerOperatingRecord>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Name { get; set; }
        public int CustomerTypeValueId { get; set; }
        public int CustomerStatusValueId { get; set; }
        public string SuperiorCustomer { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int BusinessOpportunitySourceValueId { get; set; }
        public int IndustryInvolvedValueId { get; set; }
        public int WorkforceValueId { get; set; }
        public string WeChatId { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<System.DateTime> NextTime { get; set; }
        public string ContactName { get; set; }
        public int DptId { get; set; }
        public System.Guid OwnerEmpGuid { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CustomerFollowUpRecord> CustomerFollowUpRecords { get; set; }
        public virtual ICollection<CustomerItemValue> CustomerItemValues { get; set; }
        public virtual ICollection<CustomerOperatingRecord> CustomerOperatingRecords { get; set; }
        public virtual ItemValue ItemValue { get; set; }
        public virtual ItemValue ItemValue1 { get; set; }
        public virtual ItemValue ItemValue2 { get; set; }
        public virtual ItemValue ItemValue3 { get; set; }
        public virtual ItemValue ItemValue4 { get; set; }
    }
}
