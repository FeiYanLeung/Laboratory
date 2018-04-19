using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.DbTest
{
    /// <summary>
    /// 专辑
    /// </summary>
    public class Album
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }

        /// <summary>
        /// 流派
        /// </summary>
        [DisplayName("Genre")]
        public int GenreId { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        [DisplayName("Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("Album Title")]
        [Required(ErrorMessage = "An {0} is required")]
        [StringLength(160)]
        public string Title { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
