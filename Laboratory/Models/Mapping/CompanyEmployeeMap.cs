using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class CompanyEmployeeMap : EntityTypeConfiguration<CompanyEmployee>
    {
        public CompanyEmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RealName)
                .HasMaxLength(30);

            this.Property(t => t.NickName)
                .HasMaxLength(30);

            this.Property(t => t.RemarkName)
                .HasMaxLength(30);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            this.Property(t => t.Mobile)
                .HasMaxLength(20);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.Weixin)
                .HasMaxLength(30);

            this.Property(t => t.JoinReason)
                .HasMaxLength(200);

            this.Property(t => t.LeaveReason)
                .HasMaxLength(200);

            this.Property(t => t.Reason)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CompanyEmployees");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.EmployeeGuid).HasColumnName("EmployeeGuid");
            this.Property(t => t.RealName).HasColumnName("RealName");
            this.Property(t => t.NickName).HasColumnName("NickName");
            this.Property(t => t.RemarkName).HasColumnName("RemarkName");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.UserNo).HasColumnName("UserNo");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Weixin).HasColumnName("Weixin");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.JoinReason).HasColumnName("JoinReason");
            this.Property(t => t.LeaveReason).HasColumnName("LeaveReason");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsAdmin).HasColumnName("IsAdmin");
            this.Property(t => t.CreatedOnUtc).HasColumnName("CreatedOnUtc");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedOnUtc).HasColumnName("UpdatedOnUtc");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
        }
    }
}
