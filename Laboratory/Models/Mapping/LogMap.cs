using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.IPAddress)
                .HasMaxLength(15);

            this.Property(t => t.PageUrl)
                .HasMaxLength(500);

            this.Property(t => t.Creater)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Logs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.LogLevel).HasColumnName("LogLevel");
            this.Property(t => t.LogType).HasColumnName("LogType");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.PageUrl).HasColumnName("PageUrl");
            this.Property(t => t.Creater).HasColumnName("Creater");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
        }
    }
}
