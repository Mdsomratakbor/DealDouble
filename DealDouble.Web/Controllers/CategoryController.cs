using DealDouble.Entities;
using DealDouble.Entities.Enums;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult Index(string searchTearm, int? pageSize, int? pageNo)
        {
            CategoryList model = new CategoryList();
            pageSize = pageSize.HasValue?pageSize.Value>10?pageSize.Value:10:10;
            pageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            model.Categories = CategoriesService.Instance.GetAllCategories(searchTearm, pageSize.Value, pageNo.Value);
            model.PageTitle = "Categories";
            model.PageDescription = "This is Categories list";
            var totalCategory = CategoriesService.Instance.TotalCategories(searchTearm);
            model.Pager = new Pager(totalCategory, pageNo.Value, pageSize.Value);
            model.PageSize = pageSize.Value;
            model.PageNo = pageNo.Value;
            model.SearchTearm = searchTearm;
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
            CategoryCRUDViewModel model = new CategoryCRUDViewModel();
            model.ParentCategories = ParentCategorySevices.Instance.GetAllParentCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(CategoryCRUDViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = new Category();
                    newCategory.Name = model.Name;
                    newCategory.ParentCategoryID = model.ParentCategoryID;
                    newCategory.Description = model.Description;
                    CategoriesService.Instance.SaveCategory(newCategory);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = new CategoryCRUDViewModel();
                var category = CategoriesService.Instance.GetCategoryById(id);
                model.ID = category.ID;
                model.ParentCategoryID = category.ParentCategoryID;
                model.ParentCategories = ParentCategorySevices.Instance.GetAllParentCategories();
                model.Name = category.Name;
                model.Description = category.Description;
                return PartialView(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
          
        }
        [HttpPost]
        public ActionResult Edit(CategoryCRUDViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editCategory = new Category();
                    editCategory.ID = model.ID;
                    editCategory.Name = model.Name;
                    editCategory.ParentCategoryID = model.ParentCategoryID;
                    editCategory.Description = model.Description;
                    CategoriesService.Instance.UpdateCategory(editCategory);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
          
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                CategoriesService.Instance.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}