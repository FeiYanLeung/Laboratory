using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class DepartmentMember
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int DptId { get; set; }
        public System.Guid EmpGuid { get; set; }
        public System.Guid UserGuid { get; set; }
        public bool IsDptAdmin { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual Department Department { get; set; }
    }
}
