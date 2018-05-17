using System.Collections.Generic;

namespace Laboratory.WebApi.Models.Patch
{
    public class Album
    {
        public int AlbumId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 已删除
        /// </summary>
        public int IsDeleted { get; set; } = 0;

        public List<string> Tags { get; set; }

        public virtual Genre Genre { get; set; }
    }
}