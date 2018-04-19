using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class DbBackup
    {
        public int Id { get; set; }
        public int BackupType { get; set; }
        public string DbName { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
    }
}
