using System;
using System.Collections.Generic;
using Library.Models;
using Microsoft.CodeAnalysis;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class BlogUpdateRequest
    {
        public Blog Blog { get; set; }
        public Recipe Recipe { get; set; }
        public List<RecipeDetail> RecipeDetails { get; set; }
        public List<BlogSubCate> BlogSubCates { get; set; }
        public List<BlogReferenceRequest> BlogReferences { get; set; }

        public BlogUpdateRequest()
        {
            RecipeDetails = new List<RecipeDetail>();
            BlogSubCates = new List<BlogSubCate>();
        }
        public class BlogReferenceRequest
        {
            public string Text { get; set; }
            public string HTML { get; set; }
            public int Type { get; set; }
        }
    }
}

