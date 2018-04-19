using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class CompanyEmployee
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public System.Guid EmployeeGuid { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string RemarkName { get; set; }
        public int Sex { get; set; }
        public System.Guid UserGuid { get; set; }
        public int UserNo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string QQ { get; set; }
        public string Weixin { get; set; }
        public int Status { get; set; }
        public string JoinReason { get; set; }
        public string LeaveReason { get; set; }
        public string Reason { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
        public System.Guid CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
    }
}
