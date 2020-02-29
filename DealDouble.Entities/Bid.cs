using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Bid :BaseEntity
    {
        public int AuctionID { get; set; }
        public Auction Auction { get; set; }
        public DealDoubleUser User { get; set; }
        public string UserID { get; set; }
        public double BidAmount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
