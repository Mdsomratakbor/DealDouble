using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuction { get; set; }
        public List<Auction> PromotedAuction { get; set; }
        public List<Category> Categories { get; set; }
        public string SearchTearm { get; set; }
        public int CategoryID { get; set; }
        public int PageSize { get; set; }
        public Pager Pager { get; set; }
        
    }
    public class AuctionCrudeViewModel : PageViewModel
    {
        public int AuctionID { get; set; }
        [Required(ErrorMessage ="Please Enter is Title Name")]
        [MinLength(15, ErrorMessage = "Minimum length is 15 characters")]
        [MaxLength(150, ErrorMessage = "Maximum length is 150 characters")]
       
        public string Title { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }
        [Required]
        [Range(10, 200000000, ErrorMessage = ("Acutal amount must be winthin 10-200000000"))]
        public decimal ActualAmount { get; set; }

        public DateTime? StartingTime { get; set; }

        public DateTime? EndTime { get; set; }
        public string AuctionPictures { get; set; }
        public List<AuctionPicture> AuctionImage { get; set; }
        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class AuctionDetailsViewModel : PageViewModel
    {
        public  Category Category { get; set; }
        public int CategoryID { get; set; }
        public int AuctionID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal ActualAmount { get; set; }

        public DateTime? StartingTime { get; set; }

        public DateTime? EndTime { get; set; }
        public Decimal BidsAmount { get; set; }
        public DealDoubleUser LatestBider { get; set; }
        public List<Comment> Comments { get; set; }
        public int EntityID { get; set; }


        public  List<AuctionPicture> AuctionPictures { get; set; }
    }

}