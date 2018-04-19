using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Plantform.Models.Mapping
{
    public class PerformanceGoalMap : EntityTypeConfiguration<PerformanceGoal>
    {
        public PerformanceGoalMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("PerformanceGoals");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyNo).HasColumnName("CompanyNo");
            this.Property(t => t.UserGuid).HasColumnName("UserGuid");
            this.Property(t => t.EmpGuid).HasColumnName("EmpGuid");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.ContractGoals).HasColumnName("ContractGoals");
            this.Property(t => t.PaymentGoals).HasColumnName("PaymentGoals");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateOnUtc).HasColumnName("CreateOnUtc");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.UpdateOnUtc).HasColumnName("UpdateOnUtc");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
        }
    }
}
