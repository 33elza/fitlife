namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Description");
        }
    }
}
