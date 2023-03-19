﻿using BE_Homnayangi.Modules.RecipeDetailModule.RecipeDetailsDTO;
using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.BlogModule.Response
{
    public class OverviewBlogResponse
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ListSubCateName { get; set; }
        public string ImageUrl { get; set; }
        public int TotalKcal { get; set; }

        // Recipe information
        public string RecipeTitle { get; set; }
        public decimal PackagePrice { get; set; }
        public decimal CookedPrice { get; set; }
        public List<RecipeDetailsOverview> RecipeDetails { get; set; }
    }
}
