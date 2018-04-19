using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CaptchaMap : EntityTypeConfiguration<Captcha>
    {
        public CaptchaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(15);

            this.Property(t => t.Identifier)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Captchas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Identifier).HasColumnName("Identifier");
            this.Property(t => t.ExpireTimeOnUtc).HasColumnName("ExpireTimeOnUtc");
            this.Property(t => t.CaptchaDomain).HasColumnName("CaptchaDomain");
        }
    }
}
