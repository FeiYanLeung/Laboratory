using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class FollowUpRecordAttachment
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public int FollowUpRecordId { get; set; }
        public int AttachmentId { get; set; }
        public int AttachmentType { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual FollowUpRecord FollowUpRecord { get; set; }
        public virtual SystemAttachment SystemAttachment { get; set; }
    }
}
