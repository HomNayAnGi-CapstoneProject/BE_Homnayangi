using System;

namespace BE_Homnayangi.Modules.PackageDetailModule.Response
{
    public class PackageDetailsOverview
    {
        public Guid RecipeId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
