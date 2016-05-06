namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Plans", name: "Author_Id", newName: "AuthorID");
            RenameIndex(table: "dbo.Plans", name: "IX_Author_Id", newName: "IX_AuthorID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Plans", name: "IX_AuthorID", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.Plans", name: "AuthorID", newName: "Author_Id");
        }
    }
}
