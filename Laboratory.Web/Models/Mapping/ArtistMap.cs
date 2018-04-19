using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Laboratory.Web
{
    public class ArtistMap : EntityTypeConfiguration<Artist>
    {
        public ArtistMap()
        {
            #region Table & Properties

            this.ToTable("Artists");

            this.HasKey(t => t.ArtistId);
            this.Property(t => t.ArtistId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ArtistId)
                .HasColumnName("ArtistId");

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Name");

            #endregion
        }
    }
}
