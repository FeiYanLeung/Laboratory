using System;

namespace Laboratory.AutoMapper
{
    [Serializable]
    public class SourceItem
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public virtual Source Source { get; set; }
    }
}
