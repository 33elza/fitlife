namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sets", "Weight", c => c.Int());
            AlterColumn("dbo.Sets", "Quantity", c => c.Int());
            AlterColumn("dbo.Sets", "Time", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sets", "Time", c => c.Int(nullable: false));
            AlterColumn("dbo.Sets", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Sets", "Weight", c => c.Int(nullable: false));
        }
    }
}
