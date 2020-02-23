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
        public ActionResult Index()
        {
            var model = new AuctionsViewModel();
            model.AllAuction = AuctionService.Instance.GetAllAuction();
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
            return PartialView();
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

            var pictuerIDs = model.AuctionPictures.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            auction.AuctionPictures.AddRange(pictuerIDs.Select(x => new AuctionPicture { PictureID = x }).ToList());


            AuctionService.Instance.SaveAuction(auction);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new AuctionCrudeViewModel();
            var auction = AuctionService.Instance.GetAuction(id);
            model.AuctionID = auction.AuctionID;
            model.Title = auction.Title;
            model.Description = auction.Description;
            model.StartingTime = auction.StartingTime;
            model.EndTime = auction.EndTime;
            model.ActualAmount = auction.ActualAmount;
            return PartialView(auction);
        }
        [HttpPost]
        public ActionResult Edit(AuctionCrudeViewModel model)
        {
            var editAuction = new Auction();
            editAuction.AuctionID = model.AuctionID;
            editAuction.Title = model.Title;
            editAuction.Description = model.Description;
            editAuction.StartingTime = model.StartingTime;
            editAuction.EndTime = model.EndTime;
            editAuction.ActualAmount = model.ActualAmount;
            AuctionService.Instance.UpdateAuction(editAuction);
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
            var model = new AuctionCrudeViewModel();
            var auction = AuctionService.Instance.GetAuction(id);
            model.AuctionID = auction.AuctionID;
            model.Title = auction.Title;
            model.Description = auction.Description;
            model.StartingTime = auction.StartingTime;
            model.EndTime = auction.EndTime;
            model.PageTitle = "Auction-Details";
            model.PageDescription = "This Auction Details page";
            model.ActualAmount = auction.ActualAmount;
            
           
            return View(model);
        }
      

    }
}