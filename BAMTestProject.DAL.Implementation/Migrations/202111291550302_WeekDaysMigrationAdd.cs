namespace BAMTestProject.DAL.Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeekDaysMigrationAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Broadcasts", "BroadcastDays", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Broadcasts", "BroadcastDays");
        }
    }
}
