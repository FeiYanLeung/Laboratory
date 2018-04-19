using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.DbTest
{
    /// <summary>
    /// 艺术家
    /// </summary>
    public class Artist
    {
        public Artist()
        {
            this.Albums = new List<Album>();
        }

        public int ArtistId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("艺术家名称")]
        [StringLength(50)]
        [Required(ErrorMessage = "请输入{0}")]
        public string Name { get; set; }

        /// <summary>
        /// 专辑
        /// </summary>
        public virtual List<Album> Albums { get; set; }
    }
}
