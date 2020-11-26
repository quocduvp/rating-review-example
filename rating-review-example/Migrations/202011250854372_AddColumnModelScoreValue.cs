namespace rating_review_example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnModelScoreValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScoreValue", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScoreValue", "Text");
        }
    }
}
