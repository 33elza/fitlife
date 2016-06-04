namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plans", "DifficultyLevel", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "DifficultyLevel", c => c.Int());
        }
    }
}
