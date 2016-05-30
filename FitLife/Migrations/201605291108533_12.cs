namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Set_ID", "dbo.Sets");
            DropIndex("dbo.Results", new[] { "Set_ID" });
            RenameColumn(table: "dbo.Results", name: "Set_ID", newName: "SetID");
            AlterColumn("dbo.Results", "SetID", c => c.Int(nullable: false));
            CreateIndex("dbo.Results", "SetID");
            AddForeignKey("dbo.Results", "SetID", "dbo.Sets", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "SetID", "dbo.Sets");
            DropIndex("dbo.Results", new[] { "SetID" });
            AlterColumn("dbo.Results", "SetID", c => c.Int());
            RenameColumn(table: "dbo.Results", name: "SetID", newName: "Set_ID");
            CreateIndex("dbo.Results", "Set_ID");
            AddForeignKey("dbo.Results", "Set_ID", "dbo.Sets", "ID");
        }
    }
}
