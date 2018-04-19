using System.Collections.Generic;

namespace Laboratory.AutoMapper
{
    public class Source
    {
        public Source()
        {
            this.SourceItems = new List<SourceItem>();
            this.Tags = new List<int>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<int> Tags { get; set; }
        public virtual ICollection<SourceItem> SourceItems { get; set; }
    }
}
