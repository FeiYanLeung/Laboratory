using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class RoleModuleMap : EntityTypeConfiguration<RoleModule>
    {
        public RoleModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ModuleName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ActionType)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("RoleModules");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
        }
    }
}
