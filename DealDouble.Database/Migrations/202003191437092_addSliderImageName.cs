namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSliderImageName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SliderImages", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SliderImages", "ImageName");
        }
    }
}
