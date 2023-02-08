using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule.Response
{
    public class SearchIngredientsResponse
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
    }
}
