using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTO.CategoryDTO
{
    public class CreateCategoryRequest
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
