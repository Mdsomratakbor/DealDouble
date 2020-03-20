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

        public List<Category> GetAllCategories(string searchTearm, int pageSize, int pageNo)
        {
            using (var context = new Context())
            {
               var categories =   context.Categories.Include(x => x.Auctions).ToList();
                if (!string.IsNullOrEmpty(searchTearm))
                {
                    categories = categories.Where(x => x.Name.ToLower().Contains(searchTearm.ToLower()) && x.Description.ToLower().Contains(searchTearm.ToLower())).ToList();
                }
                return categories.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int TotalCategories(string searchTearm)
        {

            using (var context = new Context())
            {
                var categories = context.Categories.Include(x => x.Auctions).ToList();
                if (!string.IsNullOrEmpty(searchTearm))
                {
                    categories = categories.Where(x => x.Name.ToLower().Contains(searchTearm.ToLower()) && x.Description.ToLower().Contains(searchTearm.ToLower())).ToList();
                }
                return categories.Count();
            }
        }

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
