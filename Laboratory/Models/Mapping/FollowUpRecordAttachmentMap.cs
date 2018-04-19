using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class FollowUpRecordAttachmentMap : EntityTypeConfiguration<FollowUpRecordAttachment>
    {
        public FollowUpRecordAttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FollowUpRecordAttachments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.FollowUpRecordId).HasColumnName("FollowUpRecordId");
            this.Property(t => t.AttachmentId).HasColumnName("AttachmentId");
            this.Property(t => t.AttachmentType).HasColumnName("AttachmentType");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.FollowUpRecord)
                .WithMany(t => t.FollowUpRecordAttachments)
                .HasForeignKey(d => d.FollowUpRecordId);
            this.HasRequired(t => t.SystemAttachment)
                .WithMany(t => t.FollowUpRecordAttachments)
                .HasForeignKey(d => d.AttachmentId);

        }
    }
}
