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
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            AuctionCrudeViewModel model = new AuctionCrudeViewModel();
            model.Categories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(AuctionCrudeViewModel model)
        {
            var auction = new Auction();
            auction.AuctionID = model.AuctionID;
            auction.Title = model.Title;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndTime = model.EndTime;
            auction.AuctionPictures = new List<AuctionPicture>();
            auction.CategoryID = model.CategoryID;
            if(model.AuctionPictures != null)
            {
                var pictuerIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                auction.AuctionPictures.AddRange(pictuerIDs.Select(x => new AuctionPicture { PictureID = x }).ToList());
            }
          


            AuctionService.Instance.SaveAuction(auction);
            return RedirectToAction("Index");
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
            auction.AuctionPictures = new List<AuctionPicture>();
            auction.CategoryID = model.CategoryID;
            if (model.AuctionPictures != null)
            {
                var pictuerIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                auction.AuctionPictures.AddRange(pictuerIDs.Select(x => new AuctionPicture {AuctionID = auction.AuctionID, PictureID = x}).ToList());
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
            
           
            return View(model);
        }
      

    }
}