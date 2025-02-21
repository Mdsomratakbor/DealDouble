﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Auction> Auctions { get; set; }
        public int ParentCategoryID { get; set; }
        public virtual ParentCategory ParentCategories {get;set;}
    }
}
