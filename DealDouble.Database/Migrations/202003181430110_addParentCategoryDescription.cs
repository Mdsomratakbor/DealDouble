namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addParentCategoryDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParentCategories", "Description", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParentCategories", "Description");
        }
    }
}
