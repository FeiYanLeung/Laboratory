using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class SystemUserMap : EntityTypeConfiguration<SystemUser>
    {
        public SystemUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RealName)
                .HasMaxLength(30);

            this.Property(t => t.Avatar)
                .HasMaxLength(500);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(150);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.PasswordFormat)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.PasswordSalt)
                .HasMaxLength(50);

            this.Property(t => t.LatestIpAddress)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SystemUsers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.UserNo).HasColumnName("UserNo");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.RealName).HasColumnName("RealName");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Avatar).HasColumnName("Avatar");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.PasswordFormat).HasColumnName("PasswordFormat");
            this.Property(t => t.PasswordFormatId).HasColumnName("PasswordFormatId");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.LatestIpAddress).HasColumnName("LatestIpAddress");
            this.Property(t => t.LatestLoginDateUtc).HasColumnName("LatestLoginDateUtc");
            this.Property(t => t.LatestActivityDateUtc).HasColumnName("LatestActivityDateUtc");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
        }
    }
}
