using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class ItemValue
    {
        public ItemValue()
        {
            this.ContactItemValues = new List<ContactItemValue>();
            this.Contacts = new List<Contact>();
            this.Contacts1 = new List<Contact>();
            this.CustomerItemValues = new List<CustomerItemValue>();
            this.Customers = new List<Customer>();
            this.Customers1 = new List<Customer>();
            this.Customers2 = new List<Customer>();
            this.Customers3 = new List<Customer>();
            this.Customers4 = new List<Customer>();
            this.FollowUpRecords = new List<FollowUpRecord>();
            this.SalesLeadItemValues = new List<SalesLeadItemValue>();
            this.SalesLeads = new List<SalesLead>();
            this.SalesLeads1 = new List<SalesLead>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int ItemId { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
        public int SortId { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<ContactItemValue> ContactItemValues { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Contact> Contacts1 { get; set; }
        public virtual ICollection<CustomerItemValue> CustomerItemValues { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Customer> Customers1 { get; set; }
        public virtual ICollection<Customer> Customers2 { get; set; }
        public virtual ICollection<Customer> Customers3 { get; set; }
        public virtual ICollection<Customer> Customers4 { get; set; }
        public virtual ICollection<FollowUpRecord> FollowUpRecords { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<SalesLeadItemValue> SalesLeadItemValues { get; set; }
        public virtual ICollection<SalesLead> SalesLeads { get; set; }
        public virtual ICollection<SalesLead> SalesLeads1 { get; set; }
    }
}
