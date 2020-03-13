namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSummaryColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "Summary");
        }
    }
}
