using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class SalesLeadItemValue
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int SalesLeadId { get; set; }
        public int ItemId { get; set; }
        public int ItemValueId { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemValue ItemValue { get; set; }
        public virtual SalesLead SalesLead { get; set; }
    }
}
