using DealDouble.Database;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DealDouble.Services
{
    public class AuctionService
    {

        #region Singletone
        public static AuctionService Instance
        {
            get
            {
                if (instance == null) instance = new AuctionService();
                return instance;
            }
        }
        private static AuctionService instance { get; set; }
        #endregion

        public int TotalItemsCount(int? categoryID, string searchTearm)
        {
            using (var context  = new Context())
            {
                var auctions = context.Auctions.AsQueryable();
                if(categoryID > 0)
                {
                    auctions = auctions.Where(x => x.CategoryID == categoryID);
                }
                if (string.IsNullOrEmpty(searchTearm) == false)
                {
                    auctions = auctions.Where(x => x.Title.ToLower().Contains(searchTearm.ToLower()));
                }
                return auctions.Count();
            }

        }

        public List<Auction> GetAllAuction(int? categoryID, string searchTearm, int pageNo, int pageSize)
        {
            using (var context = new Context())
            {
                var auctions = context.Auctions.Include(x => x.Category).Include(y => y.AuctionPictures).Include(z => z.AuctionPictures.Select(w => w.Picture)).AsQueryable();
                if (categoryID.HasValue && categoryID.Value > 0)
                {
                    auctions = auctions.Where(category => category.CategoryID == categoryID);
                }
                if (string.IsNullOrEmpty(searchTearm) == false)
                {
                    auctions = auctions.Where(auction => auction.Title.ToLower().Contains(searchTearm.ToLower()));
                }
                return auctions.OrderByDescending(x => x.CategoryID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public List<Auction> GetPromoAuction()
        {
            using (var context = new Context())
            {
                return context.Auctions.Include(x => x.Category).Include(y => y.AuctionPictures).Include(z => z.AuctionPictures.Select(w => w.Picture)).Take(4).ToList();
            }
        }
        public Auction GetAuction(int id)
        {

            using (var context = new Context())
            {
                return context.Auctions.Where(x => x.AuctionID == id).Include(x => x.Category).Include(y => y.AuctionPictures).Include(z => z.AuctionPictures.Select(w => w.Picture)).Include(o => o.Bids).Include(p => p.Bids.Select(q => q.User)).FirstOrDefault();
            }
        }
        public string SaveAuction(Auction auction)
        {
            using (var context = new Context())
            {
                try
                {
                    context.Auctions.Add(auction);
                    context.SaveChanges();
                    return "Data Save Successfull";
                }
                catch(Exception ex)
                {
                    return  ex.Message;
                }
             
            }
        }
        public void UpdateAuction(Auction auction)
        {
            using (var context = new Context())
            {
                var existingAuction = context.Auctions.Find(auction.AuctionID);
                context.AuctionPictures.RemoveRange(existingAuction.AuctionPictures);
                context.Entry(existingAuction).CurrentValues.SetValues(auction);
                context.AuctionPictures.AddRange(auction.AuctionPictures);
                context.SaveChanges();
            }

        }
        public void DeleteAuction(int id)
        {
            using (var context = new Context())
            {
                var auction = context.Auctions.Find(id);
                context.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                context.Auctions.Remove(auction);
                context.SaveChanges();
            }
        }

        public List<Auction> GetCommentAuction(List<int> auctionIds)
        {
            using (var contaxt = new Context())
            {
                return auctionIds.Select(x => contaxt.Auctions.Find(x)).ToList();
            }
        }
    }
}
