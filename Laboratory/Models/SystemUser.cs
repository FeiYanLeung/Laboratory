using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class SystemUser
    {
        public int Id { get; set; }
        public System.Guid UserGuid { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public int Sex { get; set; }
        public string Avatar { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordFormat { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string LatestIpAddress { get; set; }
        public Nullable<System.DateTime> LatestLoginDateUtc { get; set; }
        public Nullable<System.DateTime> LatestActivityDateUtc { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
    }
}
