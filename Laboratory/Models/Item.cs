using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Item
    {
        public Item()
        {
            this.ContactItemValues = new List<ContactItemValue>();
            this.CustomerItemValues = new List<CustomerItemValue>();
            this.ItemValues = new List<ItemValue>();
            this.SalesLeadItemValues = new List<SalesLeadItemValue>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ParentId { get; set; }
        public bool IsSystem { get; set; }
        public int Status { get; set; }
        public int SortId { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<ContactItemValue> ContactItemValues { get; set; }
        public virtual ICollection<CustomerItemValue> CustomerItemValues { get; set; }
        public virtual ICollection<ItemValue> ItemValues { get; set; }
        public virtual ICollection<SalesLeadItemValue> SalesLeadItemValues { get; set; }
    }
}
