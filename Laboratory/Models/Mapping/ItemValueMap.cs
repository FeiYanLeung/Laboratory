using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class ItemValueMap : EntityTypeConfiguration<ItemValue>
    {
        public ItemValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ItemValues");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.ItemId).HasColumnName("ItemId");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.SortId).HasColumnName("SortId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Item)
                .WithMany(t => t.ItemValues)
                .HasForeignKey(d => d.ItemId);

        }
    }
}
