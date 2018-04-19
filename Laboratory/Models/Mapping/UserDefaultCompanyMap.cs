using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class UserDefaultCompanyMap : EntityTypeConfiguration<UserDefaultCompany>
    {
        public UserDefaultCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserDefaultCompanies");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserNo).HasColumnName("UserNo");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
        }
    }
}
