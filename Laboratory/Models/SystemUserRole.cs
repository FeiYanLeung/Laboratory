using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class SystemUserRole
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public System.Guid UserGuid { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual Role Role { get; set; }
    }
}
