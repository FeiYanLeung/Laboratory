using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class SystemAttachmentMap : EntityTypeConfiguration<SystemAttachment>
    {
        public SystemAttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.OriginalFileName)
                .HasMaxLength(256);

            this.Property(t => t.FileName)
                .HasMaxLength(256);

            this.Property(t => t.FileDomain)
                .HasMaxLength(256);

            this.Property(t => t.FilePath)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.FileExt)
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SystemAttachments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.OriginalFileName).HasColumnName("OriginalFileName");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileDomain).HasColumnName("FileDomain");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.FileExt).HasColumnName("FileExt");
            this.Property(t => t.FileSize).HasColumnName("FileSize");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
        }
    }
}
