using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class SalesLeadItemValueMap : EntityTypeConfiguration<SalesLeadItemValue>
    {
        public SalesLeadItemValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SalesLeadItemValues");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.SalesLeadId).HasColumnName("SalesLeadId");
            this.Property(t => t.ItemId).HasColumnName("ItemId");
            this.Property(t => t.ItemValueId).HasColumnName("ItemValueId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Item)
                .WithMany(t => t.SalesLeadItemValues)
                .HasForeignKey(d => d.ItemId);
            this.HasRequired(t => t.ItemValue)
                .WithMany(t => t.SalesLeadItemValues)
                .HasForeignKey(d => d.ItemValueId);
            this.HasRequired(t => t.SalesLead)
                .WithMany(t => t.SalesLeadItemValues)
                .HasForeignKey(d => d.SalesLeadId);

        }
    }
}
