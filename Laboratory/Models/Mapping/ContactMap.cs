using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.FirstLetterName)
                .HasMaxLength(2);

            this.Property(t => t.DptName)
                .HasMaxLength(30);

            this.Property(t => t.JobTitle)
                .HasMaxLength(30);

            this.Property(t => t.MobileNumber)
                .HasMaxLength(20);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(20);

            this.Property(t => t.WeChatId)
                .HasMaxLength(32);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .HasMaxLength(60);

            this.Property(t => t.Website)
                .HasMaxLength(256);

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
            this.ToTable("Contacts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FirstLetterName).HasColumnName("FirstLetterName");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.DptName).HasColumnName("DptName");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.ImportantDegreeValueId).HasColumnName("ImportantDegreeValueId");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.WeChatId).HasColumnName("WeChatId");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.UseChineseCalendar).HasColumnName("UseChineseCalendar");
            this.Property(t => t.DecisionRelationshipValueId).HasColumnName("DecisionRelationshipValueId");
            this.Property(t => t.DptId).HasColumnName("DptId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.CustomerId);
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.DptId);
            this.HasRequired(t => t.ItemValue)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.DecisionRelationshipValueId);
            this.HasRequired(t => t.ItemValue1)
                .WithMany(t => t.Contacts1)
                .HasForeignKey(d => d.ImportantDegreeValueId);

        }
    }
}
