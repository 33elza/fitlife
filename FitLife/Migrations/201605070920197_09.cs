namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sets", "Workout_ID", "dbo.Workouts");
            DropIndex("dbo.Sets", new[] { "Workout_ID" });
            RenameColumn(table: "dbo.Sets", name: "Workout_ID", newName: "WorkoutID");
            AlterColumn("dbo.Sets", "WorkoutID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sets", "WorkoutID");
            AddForeignKey("dbo.Sets", "WorkoutID", "dbo.Workouts", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sets", "WorkoutID", "dbo.Workouts");
            DropIndex("dbo.Sets", new[] { "WorkoutID" });
            AlterColumn("dbo.Sets", "WorkoutID", c => c.Int());
            RenameColumn(table: "dbo.Sets", name: "WorkoutID", newName: "Workout_ID");
            CreateIndex("dbo.Sets", "Workout_ID");
            AddForeignKey("dbo.Sets", "Workout_ID", "dbo.Workouts", "ID");
        }
    }
}
