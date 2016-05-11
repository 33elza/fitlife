namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Workouts", name: "Plan_ID", newName: "PlanID");
            RenameIndex(table: "dbo.Workouts", name: "IX_Plan_ID", newName: "IX_PlanID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Workouts", name: "IX_PlanID", newName: "IX_Plan_ID");
            RenameColumn(table: "dbo.Workouts", name: "PlanID", newName: "Plan_ID");
        }
    }
}
