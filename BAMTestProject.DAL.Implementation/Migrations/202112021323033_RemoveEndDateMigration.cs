namespace BAMTestProject.DAL.Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEndDateMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Broadcasts", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Broadcasts", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
