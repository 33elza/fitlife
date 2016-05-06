namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workouts", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workouts", "Date", c => c.DateTime(nullable: false));
        }
    }
}
