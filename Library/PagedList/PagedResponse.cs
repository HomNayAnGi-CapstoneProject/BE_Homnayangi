using System;
using System.Collections;

namespace Library.PagedList
{
    public class PagedResponse<T>
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public ICollection Resource { get; set; }

    }
}

