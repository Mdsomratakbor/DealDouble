namespace DealDouble.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSliderImageI : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SliderImages", "Description", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SliderImages", "Description", c => c.String());
        }
    }
}
