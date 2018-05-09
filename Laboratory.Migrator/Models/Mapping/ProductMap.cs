using Laboratory.Migrator.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Laboratory.Migrator.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Products");
            this.HasKey(k => k.Id);

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasColumnName("Name")
                .IsRequired();

            this.Property(t => t.Price)
                .HasColumnName("Price")
                .IsRequired()
                .HasPrecision(7, 2);

            this.Property(t => t.CategoryId)
                .HasColumnName("CategoryId")
                .IsRequired();

            this.HasRequired(t => t.Category)
                .WithMany(m => m.Products)
                .HasForeignKey(fk => fk.CategoryId);
        }
    }
}
