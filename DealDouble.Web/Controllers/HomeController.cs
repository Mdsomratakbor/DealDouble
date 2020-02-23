using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            AuctionsViewModel model = new AuctionsViewModel();
            model.PageTitle = "Home Page";
            model.PageDescription = "This is home page";
            model.AllAuction = AuctionService.Instance.GetAllAuction();
            model.PromotedAuction = AuctionService.Instance.GetPromoAuction();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}