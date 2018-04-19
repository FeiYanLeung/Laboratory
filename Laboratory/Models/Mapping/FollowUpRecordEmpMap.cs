using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class FollowUpRecordEmpMap : EntityTypeConfiguration<FollowUpRecordEmp>
    {
        public FollowUpRecordEmpMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FollowUpRecordEmps");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.FollowUpRecordId).HasColumnName("FollowUpRecordId");
            this.Property(t => t.EmpGuid).HasColumnName("EmpGuid");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.EmpType).HasColumnName("EmpType");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.FollowUpRecord)
                .WithMany(t => t.FollowUpRecordEmps)
                .HasForeignKey(d => d.FollowUpRecordId);

        }
    }
}
