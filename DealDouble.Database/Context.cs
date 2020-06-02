using System.Data.Entity;
using DealDouble.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DealDouble.Database
{
    public class Context : IdentityDbContext<DealDoubleUser>
    {
        
        public Context():base("name = DealDoubleConnectionString")
        {
           System.Data.Entity.Database.SetInitializer<Context>(new DealDoubleDBInitializer());
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

        //    modelBuilder.Entity<DealDoubleUser>().ToTable("User");
        //    modelBuilder.Entity<IdentityRole>().ToTable("Role");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        //}

        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public static Context Create()
        {
            return new Context();
        }
    }
}
