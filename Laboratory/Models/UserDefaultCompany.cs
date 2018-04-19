using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class UserDefaultCompany
    {
        public int Id { get; set; }
        public int UserNo { get; set; }
        public System.Guid UserGuid { get; set; }
        public int CompanyNo { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
    }
}
