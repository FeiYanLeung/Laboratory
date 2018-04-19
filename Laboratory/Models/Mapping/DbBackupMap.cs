using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class DbBackupMap : EntityTypeConfiguration<DbBackup>
    {
        public DbBackupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DbName)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.FilePath)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DbBackups");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BackupType).HasColumnName("BackupType");
            this.Property(t => t.DbName).HasColumnName("DbName");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileSize).HasColumnName("FileSize");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
        }
    }
}
