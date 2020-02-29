using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [MinLength(15)]
        [MaxLength(150)]
        public string Title { get; set; }

        public  string Description { get; set; }

        [Required]
        [Range(10, 200000000, ErrorMessage = ("Acutal amount must be winthin 10-200000000"))]
        public decimal ActualAmount { get; set; }

        public DateTime? StartingTime { get; set; }

        public Nullable<DateTime> EndTime { get; set; }
        
  
        public virtual List<AuctionPicture> AuctionPictures { get; set; }    
        public virtual List<Bid> Bids { get; set; }
    }
}
