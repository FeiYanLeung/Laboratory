using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class FollowUpRecordMap : EntityTypeConfiguration<FollowUpRecord>
    {
        public FollowUpRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("FollowUpRecords");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ApproachItemValueId).HasColumnName("ApproachItemValueId");
            this.Property(t => t.FollowUpDateTime).HasColumnName("FollowUpDateTime");
            this.Property(t => t.TeamVisibleState).HasColumnName("TeamVisibleState");
            this.Property(t => t.EmpGuid).HasColumnName("EmpGuid");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.ItemValue)
                .WithMany(t => t.FollowUpRecords)
                .HasForeignKey(d => d.ApproachItemValueId);

        }
    }
}
