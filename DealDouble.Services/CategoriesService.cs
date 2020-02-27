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
    public class CategoriesService
    {

        #region Singletone
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;
            }
        }
        private static CategoriesService instance { get; set; }
        #endregion


        public List<Category> GetAllCategories()
        {
            using (var context = new Context())
            {
                return context.Categories.Include(x=>x.Auctions).ToList();
            }
        }
        //public List<Auction> GetPromoAuction()
        //{
        //    using (var context = new Context())
        //    {
        //        return context.Auctions.Take(4).ToList();
        //    }
        //}
        public Category GetCategoryById(int id)
        {

            using (var context = new Context())
            {
                return context.Categories.Find(id);
            }
        }
        public void SaveCategory(Category category)
        {
            using (var context = new Context())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new Context())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

         }
        public void DeleteCategory(int id)
        {
            using (var context = new Context())
            {
                var category = context.Categories.Find(id);
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
