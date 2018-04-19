using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Plantform.Models.Mapping;

namespace Plantform.Models
{
    public partial class LottakCRMContext : DbContext
    {
        static LottakCRMContext()
        {
            Database.SetInitializer<LottakCRMContext>(null);
        }

        public LottakCRMContext()
            : base("Name=LottakCRMContext")
        {
        }

        public DbSet<Captcha> Captchas { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<CompanyNotice> CompanyNotices { get; set; }
        public DbSet<ContactItemValue> ContactItemValues { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CustomerFollowUpRecord> CustomerFollowUpRecords { get; set; }
        public DbSet<CustomerItemValue> CustomerItemValues { get; set; }
        public DbSet<CustomerOperatingRecord> CustomerOperatingRecords { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DbBackup> DbBackups { get; set; }
        public DbSet<DepartmentMember> DepartmentMembers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<FollowUpRecordAttachment> FollowUpRecordAttachments { get; set; }
        public DbSet<FollowUpRecordEmp> FollowUpRecordEmps { get; set; }
        public DbSet<FollowUpRecord> FollowUpRecords { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemValue> ItemValues { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ModuleElement> ModuleElements { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<OperatingRecord> OperatingRecords { get; set; }
        public DbSet<PerformanceGoal> PerformanceGoals { get; set; }
        public DbSet<RoleModule> RoleModules { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SalesLeadFollowUpRecord> SalesLeadFollowUpRecords { get; set; }
        public DbSet<SalesLeadItemValue> SalesLeadItemValues { get; set; }
        public DbSet<SalesLeadOperatingRecord> SalesLeadOperatingRecords { get; set; }
        public DbSet<SalesLead> SalesLeads { get; set; }
        public DbSet<SystemAttachment> SystemAttachments { get; set; }
        public DbSet<SystemUserRole> SystemUserRoles { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<UserDefaultCompany> UserDefaultCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CaptchaMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyEmployeeMap());
            modelBuilder.Configurations.Add(new CompanyNoticeMap());
            modelBuilder.Configurations.Add(new ContactItemValueMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new CustomerFollowUpRecordMap());
            modelBuilder.Configurations.Add(new CustomerItemValueMap());
            modelBuilder.Configurations.Add(new CustomerOperatingRecordMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DbBackupMap());
            modelBuilder.Configurations.Add(new DepartmentMemberMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new FollowUpRecordAttachmentMap());
            modelBuilder.Configurations.Add(new FollowUpRecordEmpMap());
            modelBuilder.Configurations.Add(new FollowUpRecordMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new ItemValueMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new ModuleElementMap());
            modelBuilder.Configurations.Add(new ModuleMap());
            modelBuilder.Configurations.Add(new OperatingRecordMap());
            modelBuilder.Configurations.Add(new PerformanceGoalMap());
            modelBuilder.Configurations.Add(new RoleModuleMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SalesLeadFollowUpRecordMap());
            modelBuilder.Configurations.Add(new SalesLeadItemValueMap());
            modelBuilder.Configurations.Add(new SalesLeadOperatingRecordMap());
            modelBuilder.Configurations.Add(new SalesLeadMap());
            modelBuilder.Configurations.Add(new SystemAttachmentMap());
            modelBuilder.Configurations.Add(new SystemUserRoleMap());
            modelBuilder.Configurations.Add(new SystemUserMap());
            modelBuilder.Configurations.Add(new UserDefaultCompanyMap());
        }
    }
}
