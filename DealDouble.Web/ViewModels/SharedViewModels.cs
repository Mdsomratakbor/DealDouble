﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }
        public int Rating { get; set; }
    }
    public class Pictures
    {
        public string URL { get; set; }
    }
    public class Pager
    {
        public Pager(int totalItems, int? pageNo, int pageSize = 10)
        {
            if (pageSize == 0) pageSize = 10;
            var totalPages =(int) Math.Ceiling((decimal)totalItems /(decimal) pageSize);
            var currentPage = pageNo != null ? (int)pageNo : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }
        public int TotalItems { get; private set; }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public int StartPage { get; private set; }

        public int EndPage { get; private set; }
    }
}