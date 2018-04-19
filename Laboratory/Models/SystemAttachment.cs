using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class SystemAttachment
    {
        public SystemAttachment()
        {
            this.FollowUpRecordAttachments = new List<FollowUpRecordAttachment>();
        }

        public int Id { get; set; }
        public System.Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string FileDomain { get; set; }
        public string FilePath { get; set; }
        public string FileExt { get; set; }
        public Nullable<decimal> FileSize { get; set; }
        public Nullable<int> ModuleId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<FollowUpRecordAttachment> FollowUpRecordAttachments { get; set; }
    }
}
