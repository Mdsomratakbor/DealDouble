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
    public class SliderImageServices
    {

        #region Singletone
        public static SliderImageServices Instance
        {
            get
            {
                if (instance == null) instance = new SliderImageServices();
                return instance;
            }
        }
        private static SliderImageServices instance { get; set; }
        #endregion


        public List<SliderImage> GetAllSliderImage()
        {
            using (var context = new Context())
            {
                return context.SliderImages.ToList();
            }
        }
        public SliderImage GetSliderImageById(int id)
        {

            using (var context = new Context())
            {
                return context.SliderImages.Find(id);
            }
        }
        public void SaveSliderImage(SliderImage images)
        {
            using (var context = new Context())
            {
                context.SliderImages.Add(images);
                context.SaveChanges();
            }
        }
        public void UpdateSliderImage(SliderImage images)
        {
            using (var context = new Context())
            {
                context.Entry(images).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

         }
        public void DeleteSliderImages(int id)
        {
            using (var context = new Context())
            {
                var images = context.SliderImages.Find(id);
                context.Entry(images).State = System.Data.Entity.EntityState.Modified;
                context.SliderImages.Remove(images);
                context.SaveChanges();
            }
        }
    }
}
