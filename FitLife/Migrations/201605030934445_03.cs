namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Time = c.Int(nullable: false),
                        Note = c.String(),
                        Set_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sets", t => t.Set_ID)
                .Index(t => t.Set_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "Set_ID", "dbo.Sets");
            DropIndex("dbo.Results", new[] { "Set_ID" });
            DropTable("dbo.Results");
        }
    }
}
