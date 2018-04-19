using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class ContactItemValue
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int ContactId { get; set; }
        public int ItemId { get; set; }
        public int ItemValueId { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemValue ItemValue { get; set; }
    }
}
