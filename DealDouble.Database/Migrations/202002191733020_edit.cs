namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
           // DropColumn("dbo.Auctions", "EndingTime");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Auctions", "EndingTime", c => c.DateTime(nullable: false));
        }
    }
}
