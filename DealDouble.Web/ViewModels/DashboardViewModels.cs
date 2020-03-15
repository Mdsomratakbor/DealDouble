using DealDouble.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class DashboardViewModels:PageViewModel
    {
        public int UserCount { get; set; }
        public int AuctionCount { get; set; }
        public int BidCount { get; set; }
    }
    public class UserViewModel : PageViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public string SearchTearm { get; set; }
        public string RoleID { get; set; }
        public int PageNo { get; set; }
        public Pager Pager { get; set; }
        public int PageSize { get; set; }
        public List<DealDoubleUser> Users { get; set; }
    }

    public class UserDetailsViewModel : PageViewModel
    {
        public DealDoubleUser User { get; set; }

    }
}