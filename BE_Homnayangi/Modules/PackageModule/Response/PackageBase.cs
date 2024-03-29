﻿using System;

namespace BE_Homnayangi.Modules.PackageModule.Response
{
    public class PackageBase
    {
        public Guid RecipeId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? CookedPrice { get; set; }
        public int? Size { get; set; }
    }
}
