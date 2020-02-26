using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Auction
    {
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }
        public int AuctionID { get; set; }
        public string Title { get; set; }

        public  string Description { get; set; }

        public decimal ActualAmount { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndTime { get; set; }
        
  
        public virtual List<AuctionPicture> AuctionPictures { get; set; }

        //public override Category Category
        //{
        //    get
        //    {
        //        if (membership == null)
        //            membership = .Memberships.Find(CategoryID);
        //        return membership;
        //    }
        //    set { membership = value; }
        //}
    }
}
