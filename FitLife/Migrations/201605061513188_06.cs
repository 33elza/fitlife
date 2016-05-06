namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Plans", new[] { "Author_Id" });
            AlterColumn("dbo.Plans", "Author_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Plans", "Author_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Plans", new[] { "Author_Id" });
            AlterColumn("dbo.Plans", "Author_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Plans", "Author_Id");
        }
    }
}
