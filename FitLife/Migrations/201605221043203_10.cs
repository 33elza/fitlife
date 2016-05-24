namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanDTOes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Author_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfileDTOes", t => t.Author_ID)
                .Index(t => t.Author_ID);
            
            CreateTable(
                "dbo.UserProfileDTOes",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanDTOes", "Author_ID", "dbo.UserProfileDTOes");
            DropIndex("dbo.PlanDTOes", new[] { "Author_ID" });
            DropTable("dbo.UserProfileDTOes");
            DropTable("dbo.PlanDTOes");
        }
    }
}
