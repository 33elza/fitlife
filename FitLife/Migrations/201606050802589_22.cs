namespace FitLife.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanDTOes", "DifficultyLevel", c => c.String());
            AddColumn("dbo.PlanDTOes", "Sex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanDTOes", "Sex");
            DropColumn("dbo.PlanDTOes", "DifficultyLevel");
        }
    }
}
