using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class ModuleElementMap : EntityTypeConfiguration<ModuleElement>
    {
        public ModuleElementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ElementId)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ElementName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ModuleElements");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ElementId).HasColumnName("ElementId");
            this.Property(t => t.ElementName).HasColumnName("ElementName");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SortId).HasColumnName("SortId");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
        }
    }
}
