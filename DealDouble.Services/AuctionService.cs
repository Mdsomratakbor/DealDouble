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


        public List<Auction> GetAllAuction()
        {
            using (var context = new Context())
            {
                return context.Auctions.Include(x => x.Category).Include(y => y.AuctionPictures).Include(z =>z.AuctionPictures.Select(w=> w.Picture)).ToList();
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
                return context.Auctions.Where(x=>x.AuctionID == id).Include(x => x.Category).Include(y => y.AuctionPictures).Include(z => z.AuctionPictures.Select(w => w.Picture)).FirstOrDefault();
            }
        }
        public void SaveAuction(Auction auction)
        {
            using (var context = new Context())
            {
                context.Auctions.Add(auction);
                context.SaveChanges();
            }
        }
        public void UpdateAuction(Auction auction)
        {
            using (var context = new Context())
            {
                context.Entry(auction).State = System.Data.Entity.EntityState.Modified;
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
    }
}
