using System;
namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogFilterByCateAndTagRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? TagId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int sort { get; set; } 

        public BlogFilterByCateAndTagRequest()
        {
            PageNumber = 1;
            PageSize = 10;
            sort= 0; //Featured
        }
    }
}

