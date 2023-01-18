using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class PagingUserRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int sort { get; set; }

        public PagingUserRequest()
        {
            PageNumber = 1;
            PageSize = 10;
            sort = 0; //Featured
        }
    }
}
