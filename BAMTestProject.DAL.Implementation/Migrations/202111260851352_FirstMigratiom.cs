namespace BAMTestProject.DAL.Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigratiom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Broadcasts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        ShowsAmount = c.Int(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Market_Id = c.Int(),
                        Show_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Markets", t => t.Market_Id)
                .ForeignKey("dbo.Shows", t => t.Show_Id)
                .Index(t => t.Market_Id)
                .Index(t => t.Show_Id);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Broadcasts", "Show_Id", "dbo.Shows");
            DropForeignKey("dbo.Broadcasts", "Market_Id", "dbo.Markets");
            DropIndex("dbo.Broadcasts", new[] { "Show_Id" });
            DropIndex("dbo.Broadcasts", new[] { "Market_Id" });
            DropTable("dbo.Shows");
            DropTable("dbo.Markets");
            DropTable("dbo.Broadcasts");
        }
    }
}
