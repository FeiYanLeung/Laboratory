using Laboratory.Migrator.Models;
using System.Data.Entity.Migrations;

namespace Laboratory.Migrator.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false; //false为手动迁移，true为自动迁移
            ContextKey = "Laboratory.Migrator.Models.DataContext";
        }

        protected override void Seed(DataContext context) { }
    }
}
