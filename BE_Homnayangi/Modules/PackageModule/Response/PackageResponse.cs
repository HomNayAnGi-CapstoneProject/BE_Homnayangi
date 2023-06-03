using System;
using System.Collections;
using System.Collections.Generic;
using BE_Homnayangi.Modules.PackageDetailModule.Response;
using Library.Models;

namespace BE_Homnayangi.Modules.PackageModule.Response
{
    public class PackageResponse : PackageBase
    {
        public virtual Blog RecipeNavigation { get; set; }
        public virtual ICollection<PackageDetailsResponse> RecipeDetails { get; set; }

    }
}

