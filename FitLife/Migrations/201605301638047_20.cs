namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Plans", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plans", "Sex", c => c.String());
        }
    }
}
