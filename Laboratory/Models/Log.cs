using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Description { get; set; }
        public int LogLevel { get; set; }
        public int LogType { get; set; }
        public string IPAddress { get; set; }
        public string PageUrl { get; set; }
        public string Creater { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
    }
}
