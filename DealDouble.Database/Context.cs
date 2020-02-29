using DealDouble.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Database
{
    public class Context : IdentityDbContext<DealDoubleUser>
    {
        public Context():base("name = DealDoubleConnectionString")
        {

        }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public static Context Create()
        {
            return new Context();
        }
    }
}
