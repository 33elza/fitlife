namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sets", "Exercise_ID", "dbo.Exercises");
            DropIndex("dbo.Sets", new[] { "Exercise_ID" });
            RenameColumn(table: "dbo.Sets", name: "Exercise_ID", newName: "ExerciseID");
            AlterColumn("dbo.Sets", "ExerciseID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sets", "ExerciseID");
            AddForeignKey("dbo.Sets", "ExerciseID", "dbo.Exercises", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sets", "ExerciseID", "dbo.Exercises");
            DropIndex("dbo.Sets", new[] { "ExerciseID" });
            AlterColumn("dbo.Sets", "ExerciseID", c => c.Int());
            RenameColumn(table: "dbo.Sets", name: "ExerciseID", newName: "Exercise_ID");
            CreateIndex("dbo.Sets", "Exercise_ID");
            AddForeignKey("dbo.Sets", "Exercise_ID", "dbo.Exercises", "ID");
        }
    }
}
