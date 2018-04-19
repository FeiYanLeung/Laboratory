using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Domain)
                .HasMaxLength(50);

            this.Property(t => t.Abbreviation)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Address)
                .HasMaxLength(250);

            this.Property(t => t.Logo)
                .HasMaxLength(120);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            this.Property(t => t.Fax)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.QQ)
                .HasMaxLength(15);

            this.Property(t => t.Weixin)
                .HasMaxLength(30);

            this.Property(t => t.Homepage)
                .HasMaxLength(120);

            this.Property(t => t.Abstract)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Companies");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Domain).HasColumnName("Domain");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.AddressId).HasColumnName("AddressId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.CompanyType).HasColumnName("CompanyType");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Weixin).HasColumnName("Weixin");
            this.Property(t => t.Homepage).HasColumnName("Homepage");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.OwnerUserId).HasColumnName("OwnerUserId");
            this.Property(t => t.OwnerUserNo).HasColumnName("OwnerUserNo");
            this.Property(t => t.Abstract).HasColumnName("Abstract");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
        }
    }
}
