using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            CategoryList model = new CategoryList();
            model.Categories = CategoriesService.Instance.GetAllCategories();
            model.PageTitle = "Categories";
            model.PageDescription = "This is Categories list";
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(CategoryCRUDViewModel model)
        {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            CategoriesService.Instance.SaveCategory(newCategory);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new CategoryCRUDViewModel();
            var category = CategoriesService.Instance.GetCategoryById(id);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(CategoryCRUDViewModel model)
        {
            var editCategory = new Category();
            editCategory.ID = model.ID;
            editCategory.Name = model.Name;
            editCategory.Description = model.Description;
            CategoriesService.Instance.UpdateCategory(editCategory);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            CategoriesService.Instance.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}