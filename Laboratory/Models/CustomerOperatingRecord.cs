using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class CustomerOperatingRecord
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int CustomerId { get; set; }
        public int OperatingRecordId { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual OperatingRecord OperatingRecord { get; set; }
    }
}
