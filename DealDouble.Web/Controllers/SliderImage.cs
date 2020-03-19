using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{     [Authorize(Roles ="Admin")]
    public class SliderImageController : Controller
    {
        // GET: ParentCategory
        public ActionResult Index()
        {
            SliderImageList model = new SliderImageList();
            model.SliderImages = SliderImageServices.Instance.GetAllSliderImage();
            model.PageTitle = "Slider Images";
            model.PageDescription = "This is Slider Images list";
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(SliderImageViewModel model)
        {
            
            try
            {
                var newImage = new SliderImage();
                newImage.ImageName = model.ImageName;
                newImage.Description = model.Description;
                newImage.IsActive = model.IsActive;
                SliderImageServices.Instance.SaveSliderImage(newImage);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", ex.Message);
            }
        
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = new SliderImageViewModel();
                var image = SliderImageServices.Instance.GetSliderImageById(id);
                if(image != null)
                {
                    model.ID = image.ID;
                    model.Description = image.Description;
                    model.ImageName = image.ImageName;
                    model.IsActive = image.IsActive;
                    return PartialView(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", ex.Message);
            }
           
        }
        [HttpPost]
        public ActionResult Edit(SliderImageViewModel model)
        {
            try
            {
                var editImage = new SliderImage();
                editImage.ID = model.ID;
                editImage.IsActive = model.IsActive;
                if (!string.IsNullOrEmpty(model.ImageName))
                {
                    editImage.ImageName = model.ImageName;
                }
                
                editImage.Description = model.Description;
                SliderImageServices.Instance.UpdateSliderImage(editImage);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", ex.Message);
            }
     
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                SliderImageServices.Instance.DeleteSliderImages(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}