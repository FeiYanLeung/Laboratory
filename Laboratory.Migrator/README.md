Enable-Migrations
Add-Migration LabMigrator

Update-Database：开始执行迁移操作，并把数据库更新到最新的迁移文件对应的版本。

-Verbose:查看迁移在数据库中执行的详细操作(SQL等)
Update-Database -Verbose

-TargetMigration:指定目标迁移版本
Update-Database -TargetMigration Version