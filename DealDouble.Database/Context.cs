using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Database
{
    public class Context : DbContext, IDisposable
    {
        public Context():base("name = DealDoubleConnectionString")
        {

        }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionPicture> AuctionPictures { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
