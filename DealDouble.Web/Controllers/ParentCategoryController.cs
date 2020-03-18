using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{     [Authorize]
    public class ParentCategoryController : Controller
    {
        // GET: ParentCategory
        public ActionResult Index()
        {
            ParentCategoryList model = new ParentCategoryList();
            model.AllParentCategories = ParentCategorySevices.Instance.GetAllParentCategories();
            model.PageTitle = "Parent Category";
            model.PageDescription = "This is Parent Categories list";
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
        public ActionResult Create(ParentCategoryViewModel model)
        {
            
            try
            {
                var newCategory = new ParentCategory();
                newCategory.ParentCategoryName = model.ParentCategoryName;
                newCategory.Description = model.Description;
                ParentCategorySevices.Instance.SaveCategory(newCategory);
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
                var model = new ParentCategoryViewModel();
                var category = ParentCategorySevices.Instance.GetCategoryById(id);
                if(category!= null)
                {
                    model.ID = category.ID;
                    model.ParentCategoryName = category.ParentCategoryName;
                    model.Description = category.Description;
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
        public ActionResult Edit(ParentCategoryViewModel model)
        {
            try
            {
                var editCategory = new ParentCategory();
                editCategory.ID = model.ID;
                editCategory.ParentCategoryName = model.ParentCategoryName;
                editCategory.Description = model.Description;
                ParentCategorySevices.Instance.UpdateCategory(editCategory);
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
            ParentCategorySevices.Instance.DeleteCategory(id);
            return RedirectToAction("Index");
        }

    }
}