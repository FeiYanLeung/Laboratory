using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Contacts = new List<Contact>();
            this.DepartmentMembers = new List<DepartmentMember>();
            this.Departments1 = new List<Department>();
            this.SalesLeads = new List<SalesLead>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int Depth { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int SortId { get; set; }
        public Nullable<System.Guid> DptManagerEmpGuid { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<DepartmentMember> DepartmentMembers { get; set; }
        public virtual ICollection<Department> Departments1 { get; set; }
        public virtual Department Department1 { get; set; }
        public virtual ICollection<SalesLead> SalesLeads { get; set; }
    }
}
