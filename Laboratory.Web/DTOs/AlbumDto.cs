using System;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.Web
{
    [Serializable]
    public class AlbumDto
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string AlbumArtUrl { get; set; }
    }
}