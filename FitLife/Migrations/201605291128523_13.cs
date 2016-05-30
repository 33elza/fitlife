namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sets", "ResultID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sets", "ResultID");
        }
    }
}
