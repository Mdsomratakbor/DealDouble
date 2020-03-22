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
            pageSize = pageSize.HasValue ? pageSize.Value > 9 ? pageSize.Value : 9 : 9;
            model.AllAuction = AuctionService.Instance.GetAllAuction(categoryID, searchTearm, pageNo.Value, pageSize.Value);
            model.Categories = CategoriesService.Instance.GetAllCategories();
            model.PromotedAuction = AuctionService.Instance.GetPromoAuction();
            model.SliderImages = SliderImageServices.Instance.GetAllSliderImage();
            var totalpage = AuctionService.Instance.TotalItemsCount(categoryID, searchTearm);
            model.Pager = new Pager(totalpage, pageNo, pageSize.Value);
            model.SearchTearm = searchTearm;
            model.PageSize = pageSize.Value;
            model.PageNo = pageNo.Value;
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
            
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