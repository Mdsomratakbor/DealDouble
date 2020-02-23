using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuction { get; set; }
        public List<Auction> PromotedAuction { get; set; }
        
    }
    public class AuctionCrudeViewModel : PageViewModel
    {
        public int AuctionID { get; set; }
        public string Title { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public decimal ActualAmount { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndTime { get; set; }
        public string AuctionPictures { get; set; }
    }

}