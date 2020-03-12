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
       
        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public static Context Create()
        {
            return new Context();
        }
    }
}
