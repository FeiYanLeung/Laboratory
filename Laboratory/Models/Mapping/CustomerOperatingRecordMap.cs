using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CustomerOperatingRecordMap : EntityTypeConfiguration<CustomerOperatingRecord>
    {
        public CustomerOperatingRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerOperatingRecords");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.OperatingRecordId).HasColumnName("OperatingRecordId");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerOperatingRecords)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.OperatingRecord)
                .WithMany(t => t.CustomerOperatingRecords)
                .HasForeignKey(d => d.OperatingRecordId);

        }
    }
}
