using DealDouble.Database;
using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
            model.CategoryCount = DashboardServices.Instance.GetCategoryCount();
            model.RoleCount = DashboardServices.Instance.GetRoleCount();
            model.CommentCount = DashboardServices.Instance.GetCommentCount();

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
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }

        }

        public async Task<ActionResult> UserDetails(string userId, bool isPartial = false)
        {
            UserDetailsViewModel model = new UserDetailsViewModel();
            var user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                model.User = user;
            }
            if (isPartial || Request.IsAjaxRequest())
            {
                return PartialView("_UserDetails", model);
            }
            else
            {
                return View(model);
            }
        }

        public async Task<ActionResult> UserRoles(string userId, bool isPartial = false)
        {
            UserRolesViewModel model = new UserRolesViewModel();
            model.AvailableRoles = RoleManager.Roles.ToList();

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await UserManager.FindByIdAsync(userId);
                model.User = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    model.UserRoles = user.Roles.Select(userRoles => model.AvailableRoles.FirstOrDefault(role => role.Id == userRoles.RoleId)).ToList();
                }
            }

            return PartialView("_UserRoles", model);
        }


        public async Task<ActionResult> AssingUserRole(string userId, string roleId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                var user = await UserManager.FindByIdAsync(userId);
                var role = await RoleManager.FindByIdAsync(roleId);
                if (user != null && role != null)
                {
                    await UserManager.AddToRolesAsync(userId, role.Name);

                }

            }
            return RedirectToAction("UserRoles", new { userId = userId });
        }

        public async Task<ActionResult> DeleteUserRole(string userId, string roleId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                var user = await UserManager.FindByIdAsync(userId);
                var role = await RoleManager.FindByIdAsync(roleId);
                if (user != null && role != null)
                {
                    await UserManager.RemoveFromRoleAsync(userId, role.Name);

                }

            }
            return RedirectToAction("UserRoles", new { userId = userId });
        }



        public ActionResult UserRoleListing(string searchTearm, int? pageNo, int pageSize = 10)
        {
            UserRolesViewModel model = new UserRolesViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.PageTitle = "Users";
            model.PageDescription = "User List";
            model.SearchTearm = searchTearm;
            model.PageSize = pageSize;
            var roles = RoleManager.Roles.ToList();
            if (!string.IsNullOrEmpty(searchTearm))
            {
                roles = roles.Where(x => x.Name.ToLower().Contains(searchTearm.ToLower())).ToList();
            }
            var skipCount = (pageNo.Value - 1) * pageSize;
            model.AvailableRoles = roles.OrderByDescending(x => x.Id).Skip(skipCount).Take(pageSize).ToList();
            model.Pager = new Pager(roles.Count(), pageNo, pageSize);
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }

        }

        public async Task<ActionResult> UserComments(string userId, bool isPartial = false)
        {
            UserCommentsViewModel model = new UserCommentsViewModel();
            if (!string.IsNullOrEmpty(userId))
            {
                model.User = await UserManager.FindByIdAsync(userId);
                if (model.User != null)
                {
                    model.Comment = DashboardServices.Instance.GetCommentByUser(userId, (int)EntityEnum.Auctions);
                    if (model.Comment != null && model.Comment.Count > 0)
                    {
                        var auctionIDs = model.Comment.Select(x => x.RecordID).ToList();
                        model.CommentedActions = AuctionService.Instance.GetCommentAuction(auctionIDs);
                    }
                }
            }

            return PartialView("_UserComments", model);
        }

        [HttpGet]
        public ActionResult RoleCreate()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult RoleCreate(string name)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new Context()));


            if (!roleManager.RoleExists(name))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = name;
                roleManager.Create(role);

            }
            return RedirectToAction("Index");
        }

        public ActionResult CommentList(string searchTearm, int? pageNo, int? pageSize)
        {
            CommentsListViewModels model = new CommentsListViewModels();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            pageSize = pageSize.HasValue ? pageSize.Value > 10 ? pageSize.Value : 10 : 10;
            model.Comments = CommentServices.Instance.GetAllComments(searchTearm, pageNo.Value, pageSize.Value);
            model.Auctions = AuctionService.Instance.GetAllAuction();
            model.Users = UserManager.Users.ToList();
            model.PageTitle = "Comments List";
            var totalComments = CommentServices.Instance.GetTotalComments(searchTearm);
            model.PageDescription = "All Comments";
            model.Pager = new Pager(totalComments, pageNo.Value, pageSize.Value);
            model.SearchTearm = searchTearm;
            model.PageSize = pageSize.Value;
            model.PageNo = pageNo.Value;
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return View(model);
        }

        public JsonResult CommentDelete(int id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var success = CommentServices.Instance.DeleteComment(id);
            if (success)
            {
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false, Message = "There is some problem" };
            }
            return result;
        }

        [HttpGet]
        public async  Task<ActionResult> MailSend(string id)
        {
            MailSendViewModel model = new MailSendViewModel();
            var user = await UserManager.FindByIdAsync(id);
            model.UserName = user.UserName;
            model.Email = user.Email;
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult MailSend(MailSendViewModel model)
        {
            var result =  SendMail(model);
            return View(model);
        }
        public bool SendMail(MailSendViewModel model)
        {
            try
            {
                string senderMail = System.Configuration.ConfigurationManager.AppSettings["senderMail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["senderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderMail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderMail, model.Email, model.Subject, model.BodyText);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}