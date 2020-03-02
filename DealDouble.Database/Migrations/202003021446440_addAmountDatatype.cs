namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmountDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bids", "BidAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bids", "BidAmount", c => c.Double(nullable: false));
        }
    }
}
