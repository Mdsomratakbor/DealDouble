using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class CategoryList : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public Pager Pager { get; set; }
        public string SearchTearm { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
    }
    public class CategoryCRUDViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Auction> Auctions { get; set; }
        public int ParentCategoryID { get; set; }
        public  List<ParentCategory> ParentCategories { get; set; }
       
    }
}