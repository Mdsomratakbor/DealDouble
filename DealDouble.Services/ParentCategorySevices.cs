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
    public class ParentCategorySevices
    {

        #region Singletone
        public static ParentCategorySevices Instance
        {
            get
            {
                if (instance == null) instance = new ParentCategorySevices();
                return instance;
            }
        }
        private static ParentCategorySevices instance { get; set; }
        #endregion


        public List<ParentCategory> GetAllParentCategories()
        {
            using (var context = new Context())
            {
                return context.ParentCategories.Include(x=>x.Categories).ToList();
            }
        }
        public ParentCategory GetCategoryById(int id)
        {

            using (var context = new Context())
            {
                return context.ParentCategories.Find(id);
            }
        }
        public void SaveCategory(ParentCategory category)
        {
            using (var context = new Context())
            {
                context.ParentCategories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(ParentCategory category)
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
                var category = context.ParentCategories.Find(id);
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.ParentCategories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
