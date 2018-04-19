using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class SalesLeadFollowUpRecordMap : EntityTypeConfiguration<SalesLeadFollowUpRecord>
    {
        public SalesLeadFollowUpRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SalesLeadFollowUpRecords");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.SalesLoadId).HasColumnName("SalesLoadId");
            this.Property(t => t.FollowUpRecordId).HasColumnName("FollowUpRecordId");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.FollowUpRecord)
                .WithMany(t => t.SalesLeadFollowUpRecords)
                .HasForeignKey(d => d.FollowUpRecordId);
            this.HasRequired(t => t.SalesLead)
                .WithMany(t => t.SalesLeadFollowUpRecords)
                .HasForeignKey(d => d.SalesLoadId);

        }
    }
}
