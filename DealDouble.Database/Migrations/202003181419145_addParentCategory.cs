namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addParentCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentCategoryName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Categories", "ParentCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "ParentCategoryID");
            AddForeignKey("dbo.Categories", "ParentCategoryID", "dbo.ParentCategories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ParentCategoryID", "dbo.ParentCategories");
            DropIndex("dbo.Categories", new[] { "ParentCategoryID" });
            DropColumn("dbo.Categories", "ParentCategoryID");
            DropTable("dbo.ParentCategories");
        }
    }
}
