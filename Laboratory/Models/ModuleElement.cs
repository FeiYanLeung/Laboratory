using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class ModuleElement
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int ModuleId { get; set; }
        public string ElementId { get; set; }
        public string ElementName { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int SortId { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
    }
}
