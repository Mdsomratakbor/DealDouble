using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class SliderImageViewModel : PageViewModel
    {
        [Key]
        public int ID { get; set; }
        public string ImageName { get; set; }
        [MaxLength(100, ErrorMessage = "Description must be less then 100")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    public class SliderImageList:PageViewModel
    {
        public List<SliderImage> SliderImages { get; set; }
    }
}