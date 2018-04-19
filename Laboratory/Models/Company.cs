using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Company
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Domain { get; set; }
        public string Abbreviation { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.Guid> AddressId { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public int CompanyType { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Weixin { get; set; }
        public string Homepage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.Guid OwnerUserId { get; set; }
        public int OwnerUserNo { get; set; }
        public string Abstract { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
    }
}
