namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSliderImageIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SliderImages", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SliderImages", "IsActive");
        }
    }
}
