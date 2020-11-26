namespace rating_review_example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PassCode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        LoginAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PassCodeId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        ScoreId = c.Int(nullable: false),
                        Note = c.String(),
                        createdAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PassCode", t => t.PassCodeId)
                .ForeignKey("dbo.ScoreValue", t => t.ScoreId)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .Index(t => t.PassCodeId)
                .Index(t => t.ServiceId)
                .Index(t => t.ScoreId);
            
            CreateTable(
                "dbo.ScoreValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Icon = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Question = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PassCode", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.Review", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.Review", "ScoreId", "dbo.ScoreValue");
            DropForeignKey("dbo.Review", "PassCodeId", "dbo.PassCode");
            DropIndex("dbo.Service", new[] { "Name" });
            DropIndex("dbo.Review", new[] { "ScoreId" });
            DropIndex("dbo.Review", new[] { "ServiceId" });
            DropIndex("dbo.Review", new[] { "PassCodeId" });
            DropIndex("dbo.PassCode", new[] { "ServiceId" });
            DropTable("dbo.Service");
            DropTable("dbo.ScoreValue");
            DropTable("dbo.Review");
            DropTable("dbo.PassCode");
        }
    }
}
