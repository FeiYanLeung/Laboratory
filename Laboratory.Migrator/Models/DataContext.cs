using Laboratory.Core;
using Laboratory.Migrator.Models.Entities;
using System;
using System.Data.Entity;
using System.Reflection;

namespace Laboratory.Migrator.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext() : base(AppConfig.MSSQL_MIGRATOR_CONNECTION_STRING)
        {
            base.Database.Log = Console.WriteLine;

            //Database.SetInitializer(new DbInitializer.SQLDbinitializer());

            //使用自动迁移
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>(AppConfig.MSSQL_MIGRATOR_CONNECTION_STRING));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 产品
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// 产品分类
        /// </summary>
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
