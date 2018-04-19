using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class DepartmentMemberMap : EntityTypeConfiguration<DepartmentMember>
    {
        public DepartmentMemberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DepartmentMembers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.DptId).HasColumnName("DptId");
            this.Property(t => t.EmpGuid).HasColumnName("EmpGuid");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.IsDptAdmin).HasColumnName("IsDptAdmin");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.DepartmentMembers)
                .HasForeignKey(d => d.DptId);

        }
    }
}
