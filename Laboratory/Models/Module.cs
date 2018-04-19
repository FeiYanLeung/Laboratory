using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Module
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Status { get; set; }
        public bool IsVisible { get; set; }
        public int SortId { get; set; }
        public string ActionType { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
    }
}
