using Laboratory.Core;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Laboratory.DbTest
{
    public sealed class SQLiteContext : DbEntities
    {
        public SQLiteContext()
            : base("name=SQLiteContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除SQLite表名复数约定
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(SQLiteContext).Assembly);
            //通过SQLite.CodeFirst对SQLite提供CodeFirst支持。
            Database.SetInitializer(new SQLiteInitializer(modelBuilder));
        }
    }

    public sealed class SQLContext : DbEntities
    {
        public SQLContext()
            : base(AppConfig.MSSQL_CONNECTION_STRING)  //MSSQL2016_CONNECTION_STRING
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new SQLAEInitializer());
        }
    }

    public class DbEntities : DbContext
    {
        public DbEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// 专辑集合
        /// </summary>
        public virtual DbSet<Album> Albums { get; set; }

        /// <summary>
        /// 流派集合
        /// </summary>
        public virtual DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// 艺术家集合
        /// </summary>
        public virtual DbSet<Artist> Artists { get; set; }
    }
}