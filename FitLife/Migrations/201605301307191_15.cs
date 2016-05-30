namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserFollowingPlan", new[] { "FollowingPlanId" });
            DropIndex("dbo.UserFollowingPlan", new[] { "UserId" });
            RenameColumn(table: "dbo.UserFollowingPlan", name: "FollowingPlanId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.UserFollowingPlan", name: "UserId", newName: "FollowingPlanId");
            RenameColumn(table: "dbo.UserFollowingPlan", name: "__mig_tmp__0", newName: "UserId");
            DropPrimaryKey("dbo.UserFollowingPlan");
            AddColumn("dbo.Exercises", "AuthorID", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserFollowingPlan", "FollowingPlanId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserFollowingPlan", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserFollowingPlan", new[] { "UserId", "FollowingPlanId" });
            CreateIndex("dbo.Exercises", "Author_Id");
            CreateIndex("dbo.UserFollowingPlan", "UserId");
            CreateIndex("dbo.UserFollowingPlan", "FollowingPlanId");
            AddForeignKey("dbo.Exercises", "Author_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserFollowingPlan", new[] { "FollowingPlanId" });
            DropIndex("dbo.UserFollowingPlan", new[] { "UserId" });
            DropIndex("dbo.Exercises", new[] { "Author_Id" });
            DropPrimaryKey("dbo.UserFollowingPlan");
            AlterColumn("dbo.UserFollowingPlan", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserFollowingPlan", "FollowingPlanId", c => c.Int(nullable: false));
            DropColumn("dbo.Exercises", "Author_Id");
            DropColumn("dbo.Exercises", "AuthorID");
            AddPrimaryKey("dbo.UserFollowingPlan", new[] { "FollowingPlanId", "UserId" });
            RenameColumn(table: "dbo.UserFollowingPlan", name: "UserId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.UserFollowingPlan", name: "FollowingPlanId", newName: "UserId");
            RenameColumn(table: "dbo.UserFollowingPlan", name: "__mig_tmp__0", newName: "FollowingPlanId");
            CreateIndex("dbo.UserFollowingPlan", "UserId");
            CreateIndex("dbo.UserFollowingPlan", "FollowingPlanId");
        }
    }
}
