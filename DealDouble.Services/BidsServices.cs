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
    public class BidsServices
    {

        #region Singletone
        public static BidsServices Instance
        {
            get
            {
                if (instance == null) instance = new BidsServices();
                return instance;
            }
        }
        private static BidsServices instance { get; set; }
        #endregion


        public bool AddBid(Bid bid)
        {
            using (var context = new Context())
            {
                 context.Bids.Add(bid);
                return context.SaveChanges() > 0;
            }
        }
        //public List<Auction> GetPromoAuction()
        //{
        //    using (var context = new Context())
        //    {
        //        return context.Auctions.Take(4).ToList();
        //    }
        //}
        //public Category GetCategoryById(int id)
        //{

        //    using (var context = new Context())
        //    {
        //        return context.Categories.Find(id);
        //    }
        //}
        //public void SaveCategory(Category category)
        //{
        //    using (var context = new Context())
        //    {
        //        context.Categories.Add(category);
        //        context.SaveChanges();
        //    }
        //}
        //public void UpdateCategory(Category category)
        //{
        //    using (var context = new Context())
        //    {
        //        context.Entry(category).State = System.Data.Entity.EntityState.Modified;
        //        context.SaveChanges();
        //    }

        // }
        //public void DeleteCategory(int id)
        //{
        //    using (var context = new Context())
        //    {
        //        var category = context.Categories.Find(id);
        //        context.Entry(category).State = System.Data.Entity.EntityState.Modified;
        //        context.Categories.Remove(category);
        //        context.SaveChanges();
        //    }
        //}
    }
}
