namespace BAMTestProject.DAL.Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalForeignKeyRemoveMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets");
            DropForeignKey("dbo.Broadcasts", "ShowId", "dbo.Shows");
            DropIndex("dbo.Broadcasts", new[] { "ShowId" });
            DropIndex("dbo.Broadcasts", new[] { "MarketId" });
            AlterColumn("dbo.Broadcasts", "ShowId", c => c.Int(nullable: false));
            AlterColumn("dbo.Broadcasts", "MarketId", c => c.Int(nullable: false));
            CreateIndex("dbo.Broadcasts", "ShowId");
            CreateIndex("dbo.Broadcasts", "MarketId");
            AddForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Broadcasts", "ShowId", "dbo.Shows", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Broadcasts", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets");
            DropIndex("dbo.Broadcasts", new[] { "MarketId" });
            DropIndex("dbo.Broadcasts", new[] { "ShowId" });
            AlterColumn("dbo.Broadcasts", "MarketId", c => c.Int());
            AlterColumn("dbo.Broadcasts", "ShowId", c => c.Int());
            CreateIndex("dbo.Broadcasts", "MarketId");
            CreateIndex("dbo.Broadcasts", "ShowId");
            AddForeignKey("dbo.Broadcasts", "ShowId", "dbo.Shows", "Id");
            AddForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets", "Id");
        }
    }
}
