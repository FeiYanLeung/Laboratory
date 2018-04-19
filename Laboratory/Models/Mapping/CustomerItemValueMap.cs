using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CustomerItemValueMap : EntityTypeConfiguration<CustomerItemValue>
    {
        public CustomerItemValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerItemValues");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ItemId).HasColumnName("ItemId");
            this.Property(t => t.ItemValueId).HasColumnName("ItemValueId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerItemValues)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.Item)
                .WithMany(t => t.CustomerItemValues)
                .HasForeignKey(d => d.ItemId);
            this.HasRequired(t => t.ItemValue)
                .WithMany(t => t.CustomerItemValues)
                .HasForeignKey(d => d.ItemValueId);

        }
    }
}
