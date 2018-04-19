using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class OperatingRecord
    {
        public OperatingRecord()
        {
            this.CustomerOperatingRecords = new List<CustomerOperatingRecord>();
            this.SalesLeadOperatingRecords = new List<SalesLeadOperatingRecord>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Content { get; set; }
        public System.Guid EmpGuid { get; set; }
        public System.Guid UserGuid { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<CustomerOperatingRecord> CustomerOperatingRecords { get; set; }
        public virtual ICollection<SalesLeadOperatingRecord> SalesLeadOperatingRecords { get; set; }
    }
}
