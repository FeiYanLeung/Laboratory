using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class RoleModule
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int RoleId { get; set; }
        public string ModuleName { get; set; }
        public string ActionType { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
    }
}
