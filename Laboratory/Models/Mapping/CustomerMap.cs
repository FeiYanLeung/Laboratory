using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.SuperiorCustomer)
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

            this.Property(t => t.Area)
                .HasMaxLength(20);

            this.Property(t => t.Address)
                .HasMaxLength(120);

            this.Property(t => t.Postcode)
                .HasMaxLength(12);

            this.Property(t => t.FaxNumber)
                .HasMaxLength(20);

            this.Property(t => t.ContactName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Customers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CustomerTypeValueId).HasColumnName("CustomerTypeValueId");
            this.Property(t => t.CustomerStatusValueId).HasColumnName("CustomerStatusValueId");
            this.Property(t => t.SuperiorCustomer).HasColumnName("SuperiorCustomer");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.BusinessOpportunitySourceValueId).HasColumnName("BusinessOpportunitySourceValueId");
            this.Property(t => t.IndustryInvolvedValueId).HasColumnName("IndustryInvolvedValueId");
            this.Property(t => t.WorkforceValueId).HasColumnName("WorkforceValueId");
            this.Property(t => t.WeChatId).HasColumnName("WeChatId");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Postcode).HasColumnName("Postcode");
            this.Property(t => t.FaxNumber).HasColumnName("FaxNumber");
            this.Property(t => t.NextTime).HasColumnName("NextTime");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.DptId).HasColumnName("DptId");
            this.Property(t => t.OwnerEmpGuid).HasColumnName("OwnerEmpGuid");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.ItemValue)
                .WithMany(t => t.Customers)
                .HasForeignKey(d => d.BusinessOpportunitySourceValueId);
            this.HasRequired(t => t.ItemValue1)
                .WithMany(t => t.Customers1)
                .HasForeignKey(d => d.CustomerStatusValueId);
            this.HasRequired(t => t.ItemValue2)
                .WithMany(t => t.Customers2)
                .HasForeignKey(d => d.CustomerTypeValueId);
            this.HasRequired(t => t.ItemValue3)
                .WithMany(t => t.Customers3)
                .HasForeignKey(d => d.IndustryInvolvedValueId);
            this.HasRequired(t => t.ItemValue4)
                .WithMany(t => t.Customers4)
                .HasForeignKey(d => d.WorkforceValueId);

        }
    }
}
