using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class SalesLeadMap : EntityTypeConfiguration<SalesLead>
    {
        public SalesLeadMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.DptName)
                .HasMaxLength(30);

            this.Property(t => t.JobTitle)
                .HasMaxLength(30);

            this.Property(t => t.MobileNumber)
                .HasMaxLength(20);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .HasMaxLength(60);

            this.Property(t => t.WeChatId)
                .HasMaxLength(20);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.Province)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Address)
                .HasMaxLength(120);

            this.Property(t => t.Postcode)
                .HasMaxLength(12);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SalesLeads");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.DptName).HasColumnName("DptName");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.WeChatId).HasColumnName("WeChatId");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
            this.Property(t => t.OwnerEmpGuid).HasColumnName("OwnerEmpGuid");
            this.Property(t => t.DptId).HasColumnName("DptId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FollowUpStatusValueId).HasColumnName("FollowUpStatusValueId");
            this.Property(t => t.FollowUpDateTime).HasColumnName("FollowUpDateTime");
            this.Property(t => t.ClueSourceValueId).HasColumnName("ClueSourceValueId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.SalesLeads)
                .HasForeignKey(d => d.DptId);
            this.HasRequired(t => t.ItemValue)
                .WithMany(t => t.SalesLeads)
                .HasForeignKey(d => d.ClueSourceValueId);
            this.HasOptional(t => t.ItemValue1)
                .WithMany(t => t.SalesLeads1)
                .HasForeignKey(d => d.FollowUpStatusValueId);

        }
    }
}
