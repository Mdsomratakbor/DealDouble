namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationAdd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auctions", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Auctions", "StartingTime", c => c.DateTime());
            AlterColumn("dbo.Auctions", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auctions", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "StartingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "Title", c => c.String());
        }
    }
}
