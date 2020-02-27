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

        public ActionResult Index(int? categoryID, string searchTearm, int? pageNo, int? pageSize)
        {
            AuctionsViewModel model = new AuctionsViewModel();
            model.PageTitle = "Home Page";
            model.PageDescription = "This is home page";
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            pageSize = pageSize.HasValue ? pageSize.Value > 10 ? pageSize.Value : 10 : 10;
            model.AllAuction = AuctionService.Instance.GetAllAuction(categoryID, searchTearm, pageNo.Value, pageSize.Value);
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