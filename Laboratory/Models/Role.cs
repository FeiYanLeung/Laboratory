using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Role
    {
        public Role()
        {
            this.SystemUserRoles = new List<SystemUserRole>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public bool IsSystemRole { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; }
    }
}
