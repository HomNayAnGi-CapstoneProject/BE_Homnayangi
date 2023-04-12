using Library.PagedList;
using System;

namespace BE_Homnayangi.Modules.IngredientModule.Request
{
    public class IngredientsByTypeRequest: PagedRequest
    {
        public Guid? TypeId { get; set; }
        public string? SearchString { get; set; }

        public IngredientsByTypeRequest() : base() { }
    }
}
