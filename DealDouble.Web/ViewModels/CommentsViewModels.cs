using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class CommentsListViewModels : PageViewModel
    {
        public List<Comment> Comments { get; set; }
        public List<Auction> Auctions { get; set; }
        public List<DealDoubleUser> Users { get; set; }
        public Pager Pager { get; set; }
        public string SearchTearm { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
    }
    public class MailSendViewModel:PageViewModel
    {
        public string UserId { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string BodyText { get; set; }
    }
}