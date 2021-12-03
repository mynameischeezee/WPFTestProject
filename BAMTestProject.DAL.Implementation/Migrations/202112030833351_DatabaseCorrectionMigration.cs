namespace BAMTestProject.DAL.Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseCorrectionMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Broadcasts", newName: "BroadcastEntities");
            RenameTable(name: "dbo.Markets", newName: "MarketEntities");
            RenameTable(name: "dbo.Shows", newName: "ShowEntities");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ShowEntities", newName: "Shows");
            RenameTable(name: "dbo.MarketEntities", newName: "Markets");
            RenameTable(name: "dbo.BroadcastEntities", newName: "Broadcasts");
        }
    }
}
