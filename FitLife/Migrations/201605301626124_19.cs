namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "Sex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plans", "Sex");
        }
    }
}
