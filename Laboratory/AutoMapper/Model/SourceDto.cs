using System.Collections.Generic;

namespace Laboratory.AutoMapper
{
    public class BaseSourceDto
    {
        public virtual int id { get; set; }
        public virtual string title { get; set; }
    }
    public class SourceDto : BaseSourceDto
    {
        public SourceDto()
        {
            this.source_items = new List<string>();
        }
        public override string title { get; set; }
        public string tags { get; set; }
        public virtual ICollection<string> source_items { get; set; }
    }
}
