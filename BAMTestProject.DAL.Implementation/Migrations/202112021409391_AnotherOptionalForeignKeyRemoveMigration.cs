namespace BAMTestProject.DAL.Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherOptionalForeignKeyRemoveMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets");
            DropIndex("dbo.Broadcasts", new[] { "MarketId" });
            AlterColumn("dbo.Broadcasts", "MarketId", c => c.Int());
            CreateIndex("dbo.Broadcasts", "MarketId");
            AddForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets");
            DropIndex("dbo.Broadcasts", new[] { "MarketId" });
            AlterColumn("dbo.Broadcasts", "MarketId", c => c.Int(nullable: false));
            CreateIndex("dbo.Broadcasts", "MarketId");
            AddForeignKey("dbo.Broadcasts", "MarketId", "dbo.Markets", "Id", cascadeDelete: true);
        }
    }
}
