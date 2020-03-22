using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AuctionController : Controller
    {
               // GET: Auction

        [HttpGet]
        public ActionResult Index(int? categoryID, string searchTearm, int? pageNo, int? pageSize)
        {
            var model = new AuctionsViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            pageSize = pageSize.HasValue ? pageSize.Value > 10 ? pageSize.Value : 10 : 10;
            model.AllAuction = AuctionService.Instance.GetAllAuction(categoryID, searchTearm, pageNo.Value, pageSize.Value);
            model.Categories = CategoriesService.Instance.GetAllCategories();
            model.PageSize = pageSize.Value;
            model.SearchTearm = searchTearm;
            if(string.IsNullOrEmpty(categoryID.ToString()) == false) model.CategoryID = categoryID.Value;
            var totalItem = AuctionService.Instance.TotalItemsCount(categoryID, searchTearm);
            if(model.AllAuction !=null && model.AllAuction.Count > 0)
            {
                model.Pager = new Pager(totalItem, pageNo.Value, pageSize.Value);
            }
            model.PageTitle = "Auctions";
            model.PageDescription = "This auctions list";
            
            if (Request.IsAjaxRequest()){
                return PartialView(model);
            }
            else
            {
                return View(model);

            }
                
           
            
        }
        public PartialViewResult Listing(int? categoryId, string search, int? pageNo)
        {
            var pageSize = 5;
            pageNo = pageNo ?? 1;

            var auctionsModel = new AuctionsViewModel();

            auctionsModel.AllAuction = AuctionService.Instance.GetAllAuction(categoryId, search, pageNo.Value, pageSize);

            var totalAuctions = AuctionService.Instance.TotalItemsCount(categoryId, search);

            auctionsModel.Pager = new Pager(totalAuctions, pageNo, pageSize);

            auctionsModel.CategoryID = categoryId.Value;
            auctionsModel.SearchTearm = search;

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AuctionCrudeViewModel model = new AuctionCrudeViewModel();
            model.Categories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult Create(AuctionCrudeViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; 
            try
            {
                var auction = new Auction();
                if (ModelState.IsValid)
                {
                    auction.AuctionID = model.AuctionID;
                    auction.Title = model.Title;
                    auction.Description = model.Description;
                    auction.ActualAmount = model.ActualAmount;
                    auction.StartingTime = model.StartingTime;
                    auction.EndTime = model.EndTime;
                    auction.AuctionPictures = new List<AuctionPicture>();
                    auction.Summary = model.Summary ;
                    auction.CategoryID = model.CategoryID;
                    if (model.AuctionPictures != null)
                    {
                        var pictuerIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                        auction.AuctionPictures.AddRange(pictuerIDs.Select(x => new AuctionPicture { PictureID = x }).ToList());
                    }
                    AuctionService.Instance.SaveAuction(auction);
                    result.Data = new { Success = true };
                }
                else
                {
                    result.Data = new { Success = false, Error = "Unable to save Autons. Please enter valid data ."};
                }
                return result;            
            }
            catch(Exception ex)
            {
                result.Data = new { Error = ex.Message};
                return result;
            }
           
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new AuctionCrudeViewModel();
            var auction = AuctionService.Instance.GetAuction(id);
            model.Categories = CategoriesService.Instance.GetAllCategories();
            model.AuctionID = auction.AuctionID;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.Description = auction.Description;
            model.StartingTime = auction.StartingTime;
            model.EndTime = auction.EndTime;
            model.ActualAmount = auction.ActualAmount;
            model.AuctionImage = auction.AuctionPictures;
            model.Summary = auction.Summary;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(AuctionCrudeViewModel model)
        {
            var auction = new Auction();
            auction.AuctionID = model.AuctionID;
            auction.Title = model.Title;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndTime = model.EndTime;
            auction.Summary = model.Summary;
            auction.AuctionPictures = new List<AuctionPicture>();
            auction.CategoryID = model.CategoryID;
            if (model.AuctionPictures != null)
            {
                var pictuerIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                if(pictuerIDs != null && pictuerIDs.Count>0)
                {
                    auction.AuctionPictures.AddRange(pictuerIDs.Select(x => new AuctionPicture { AuctionID = auction.AuctionID, PictureID = x }).ToList());
                }
             
            }
            AuctionService.Instance.UpdateAuction(auction);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AuctionService.Instance.DeleteAuction(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                 var model = new AuctionDetailsViewModel();
            var auction = AuctionService.Instance.GetAuction(id);
            model.AuctionID = auction.AuctionID;
            model.Title = auction.Title;
            model.Description = auction.Description;
            model.StartingTime = auction.StartingTime;
            model.EndTime = auction.EndTime;
            model.PageTitle = "Auction-Details";
            model.PageDescription = "This Auction Details page";
            model.ActualAmount = auction.ActualAmount;
            model.AuctionPictures = auction.AuctionPictures;
            model.BidsAmount = auction.ActualAmount + auction.Bids.Sum(x => x.BidAmount);
            var latestBider = auction.Bids.OrderByDescending(x => x.TimeStamp).FirstOrDefault();
            model.LatestBider = latestBider != null ? latestBider.User : null;
            model.EntityID = (int)EntityEnum.Auctions;
            model.Comments = SharedService.Instance.GetCommetns(model.EntityID, model.AuctionID);
            
            
           
            return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
      

    }
}