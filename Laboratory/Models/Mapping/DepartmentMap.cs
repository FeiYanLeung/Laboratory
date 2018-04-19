using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Departments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Depth).HasColumnName("Depth");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SortId).HasColumnName("SortId");
            this.Property(t => t.DptManagerEmpGuid).HasColumnName("DptManagerEmpGuid");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasOptional(t => t.Department1)
                .WithMany(t => t.Departments1)
                .HasForeignKey(d => d.ParentId);

        }
    }
}
