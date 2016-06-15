namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "UserID");
        }
    }
}
