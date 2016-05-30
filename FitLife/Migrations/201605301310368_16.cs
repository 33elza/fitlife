namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Exercises", new[] { "Author_Id" });
            DropColumn("dbo.Exercises", "AuthorID");
            RenameColumn(table: "dbo.Exercises", name: "Author_Id", newName: "AuthorID");
            AlterColumn("dbo.Exercises", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Exercises", "AuthorID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Exercises", new[] { "AuthorID" });
            AlterColumn("dbo.Exercises", "AuthorID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Exercises", name: "AuthorID", newName: "Author_Id");
            AddColumn("dbo.Exercises", "AuthorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Exercises", "Author_Id");
        }
    }
}
