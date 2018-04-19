using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Laboratory.DbTest.Mapping
{
    public class AlbumMap : EntityTypeConfiguration<Album>
    {
        public AlbumMap()
        {
            #region Table & Properties

            this.ToTable("Albums");

            this.HasKey(t => t.AlbumId);
            this.Property(t => t.AlbumId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AlbumId)
                .HasColumnName("AlbumId");

            this.Property(t => t.GenreId)
                .HasColumnName("GenreId");

            this.Property(t => t.ArtistId)
                .HasColumnName("ArtistId");

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(160)
                .HasColumnName("Title");

            this.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price");

            this.Property(t => t.AlbumArtUrl)
                .IsOptional()
                .HasMaxLength(1024)
                .HasColumnName("AlbumArtUrl");

            #endregion

            #region Relationships

            this.HasRequired(t => t.Genre)
                .WithMany(m => m.Albums)
                .HasForeignKey(fk => fk.GenreId);

            this.HasRequired(t => t.Artist)
                .WithMany(m => m.Albums)
                .HasForeignKey(fk => fk.ArtistId);

            #endregion
        }
    }
}
