namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserFollowingPlan");
            AddPrimaryKey("dbo.UserFollowingPlan", new[] { "FollowingPlanId", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserFollowingPlan");
            AddPrimaryKey("dbo.UserFollowingPlan", new[] { "UserId", "FollowingPlanId" });
        }
    }
}
