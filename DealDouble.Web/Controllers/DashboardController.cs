using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardViewModels model = new DashboardViewModels();
            model.AuctionCount = DashboardServices.Instance.GetAuctionCount();
            model.UserCount = DashboardServices.Instance.GetUserCount();
            model.BidCount = DashboardServices.Instance.GetBidCount();

            return View(model);
        }
        public ActionResult Users(string roleID, string searchTearm, int? pageNo, int pageSize )
        {
            UserViewModel model = new UserViewModel();
            model.PageTitle = "Users";
            model.PageDescription = "User List";
            model.Roles = new List<IdentityRole>();
            model.SearchTearm = searchTearm;
            model.RoleID = roleID;
            model.PageNo = pageNo.HasValue ? pageNo.Value : 1;
            model.PageSize = pageSize;
            
            return View(model);
        }
    }
}