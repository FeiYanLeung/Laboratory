using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Controller)
                .HasMaxLength(120);

            this.Property(t => t.Action)
                .HasMaxLength(120);

            this.Property(t => t.ActionType)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Modules");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.Controller).HasColumnName("Controller");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.SortId).HasColumnName("SortId");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
        }
    }
}
