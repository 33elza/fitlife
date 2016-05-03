namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Plan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Plans", t => t.Plan_ID)
                .Index(t => t.Plan_ID);
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Time = c.Int(nullable: false),
                        Description = c.String(),
                        Exercise_ID = c.Int(),
                        Workout_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ID)
                .ForeignKey("dbo.Workouts", t => t.Workout_ID)
                .Index(t => t.Exercise_ID)
                .Index(t => t.Workout_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sets", "Workout_ID", "dbo.Workouts");
            DropForeignKey("dbo.Sets", "Exercise_ID", "dbo.Exercises");
            DropForeignKey("dbo.Workouts", "Plan_ID", "dbo.Plans");
            DropIndex("dbo.Sets", new[] { "Workout_ID" });
            DropIndex("dbo.Sets", new[] { "Exercise_ID" });
            DropIndex("dbo.Workouts", new[] { "Plan_ID" });
            DropTable("dbo.Sets");
            DropTable("dbo.Workouts");
        }
    }
}
