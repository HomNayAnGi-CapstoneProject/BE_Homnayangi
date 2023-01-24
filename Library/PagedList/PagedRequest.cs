using System;
namespace Library.PagedList
{
    public class PagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int sort { get; set; }
        public bool sortDesc { get; set; }

        public PagedRequest()
        {
            PageNumber = 1;
            PageSize = 10;
            sort = 0;
            sortDesc = true; //default
        }
    }
}

