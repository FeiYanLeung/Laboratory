using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.Web
{
    /// <summary>
    /// 流派
    /// </summary>
    public class Genre
    {
        public Genre()
        {
            this.Albums = new List<Album>();
        }

        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Album> Albums { get; set; }
    }
}
