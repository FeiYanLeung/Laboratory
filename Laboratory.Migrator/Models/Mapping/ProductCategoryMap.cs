using Laboratory.Migrator.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Laboratory.Migrator.Models.Mapping
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            this.ToTable("ProductCategories");

            this.HasKey(t => t.Id);

            this.Property(t => t.Id)
                .HasColumnName("Id");

            this.Property(t => t.ParentId)
                .HasColumnName("ParentId")
                .IsOptional();

            this.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(30);

            this.HasOptional(t => t.ParentCategory)
                .WithMany(t => t.SubCategories)
                .HasForeignKey(d => d.ParentId);
        }
    }
}
