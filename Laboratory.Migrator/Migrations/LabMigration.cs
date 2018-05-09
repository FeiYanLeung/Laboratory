using System.Data.Entity.Migrations;

namespace Laboratory.Migrator.Migrations
{
    /// <summary>
    /// 数据迁移
    /// </summary>
    public partial class LabMigration : DbMigration
    {
        /// <summary>
        /// 升级
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 120));
        }

        /// <summary>
        /// 降级
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
