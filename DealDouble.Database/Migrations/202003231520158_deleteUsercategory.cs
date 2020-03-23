namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteUsercategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Comment_ID", "dbo.Comments");
            DropIndex("dbo.AspNetUsers", new[] { "Comment_ID" });
            DropColumn("dbo.AspNetUsers", "Comment_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Comment_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Comment_ID");
            AddForeignKey("dbo.AspNetUsers", "Comment_ID", "dbo.Comments", "ID");
        }
    }
}
