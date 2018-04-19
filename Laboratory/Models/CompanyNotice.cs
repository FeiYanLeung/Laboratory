using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class CompanyNotice
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Title { get; set; }
        public string NoticeContent { get; set; }
        public string NoticeType { get; set; }
        public string PublishOrg { get; set; }
        public int Status { get; set; }
        public string RealName { get; set; }
        public int SortId { get; set; }
        public int Hits { get; set; }
        public bool IsTop { get; set; }
        public Nullable<System.DateTime> EndDateUtc { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
    }
}
