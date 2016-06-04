namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plans", "DifficultyLevel", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "DifficultyLevel", c => c.String());
        }
    }
}
