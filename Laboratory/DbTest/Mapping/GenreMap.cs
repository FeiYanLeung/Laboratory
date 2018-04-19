using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Laboratory.DbTest.Mapping
{
    public class GenreMap : EntityTypeConfiguration<Genre>
    {
        public GenreMap()
        {
            #region Tables & Properties

            this.ToTable("Genres");

            this.HasKey(t => t.GenreId);
            this.Property(t => t.GenreId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.GenreId)
                .HasColumnName("GenreId");

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Name");

            this.Property(t => t.Description)
                .IsOptional()
                .HasMaxLength(1024)
                .HasColumnName("Description");

            #endregion
        }
    }
}
