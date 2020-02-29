using DealDouble.Entities;
using DealDouble.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    //[Authorize]
    public class BidsController : Controller
    {
        // GET: Bids
        [HttpPost]
        public JsonResult AddBid(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (User.Identity.IsAuthenticated)
            {
                Bid bid = new Bid();
                bid.AuctionID = Id;
                bid.UserID = User.Identity.GetUserId();
                bid.TimeStamp = DateTime.Now;
                bid.BidAmount = 10;
               var bidResult =  BidsServices.Instance.AddBid(bid);
                if (bidResult)
                {
                    result.Data = new { Success = true };
                }
                else
                {
                    result.Data = new {Success=false, Message= "Unable to add Bid to auction"};
                }
            }
            else
            {
                result.Data = new { Success = false, Message = "User neet to login for Bids" };
            }
       
            return result;
        }
    }
}