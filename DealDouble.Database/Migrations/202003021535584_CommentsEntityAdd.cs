namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsEntityAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 600),
                        UserId = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        EntityID = c.Int(nullable: false),
                        RecordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comments");
        }
    }
}
