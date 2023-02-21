using System;
using System.Collections.Generic;
using Library.Models;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogUpdateRequest
    {
        public Blog Blog { get; set; }
        public Recipe Recipe { get; set; }
        public List<RecipeDetail> RecipeDetails { get; set; }
        public List<BlogSubCate> BlogSubCates { get; set; }
        public List<BlogReference> BlogReferences { get; set; }

        public BlogUpdateRequest()
        {
            RecipeDetails = new List<RecipeDetail>();
            BlogSubCates = new List<BlogSubCate>();
            BlogReferences = new List<BlogReference>();
        }
    }
}

