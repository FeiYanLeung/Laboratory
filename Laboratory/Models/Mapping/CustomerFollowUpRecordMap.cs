using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CustomerFollowUpRecordMap : EntityTypeConfiguration<CustomerFollowUpRecord>
    {
        public CustomerFollowUpRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerFollowUpRecords");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.FollowUpRecordId).HasColumnName("FollowUpRecordId");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerFollowUpRecords)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.FollowUpRecord)
                .WithMany(t => t.CustomerFollowUpRecords)
                .HasForeignKey(d => d.FollowUpRecordId);

        }
    }
}
