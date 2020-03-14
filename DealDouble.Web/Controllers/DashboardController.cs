using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class DashboardController : Controller
    {
        private DealDoubleUserManager _userManager;
        private DealDoubleRoleManager _roleManager;

        public DashboardController()
        {
        }

        public DashboardController(DealDoubleUserManager userManager, DealDoubleRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }


        public DealDoubleRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<DealDoubleRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public DealDoubleUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<DealDoubleUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardViewModels model = new DashboardViewModels();
            model.AuctionCount = DashboardServices.Instance.GetAuctionCount();
            model.UserCount = DashboardServices.Instance.GetUserCount();
            model.BidCount = DashboardServices.Instance.GetBidCount();

            return View(model);
        }
        public ActionResult Users(string roleID, string searchTearm, int? pageNo, int pageSize = 10)
        {
            UserViewModel model = new UserViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.PageTitle = "Users";
            model.PageDescription = "User List";
            model.Roles = RoleManager.Roles.ToList();
            model.SearchTearm = searchTearm;
            model.RoleID = roleID;
            model.PageSize = pageSize;
            // model.Users = UserManager.Users.ToList();

            var users = UserManager.Users;
            if (!string.IsNullOrEmpty(roleID))
            {
                users = users.Where(x => x.Roles.FirstOrDefault(y => y.RoleId == roleID) != null);
            }
            if (!string.IsNullOrEmpty(searchTearm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(searchTearm.ToLower()));
            }
            var skipCount = (pageNo.Value - 1) * pageSize;
            model.Users = users.OrderByDescending(x => x.Id).Skip(skipCount).Take(pageSize).ToList();
            model.Pager = new Pager(users.Count(), pageNo, pageSize);

            return View(model);
        }
    }
}