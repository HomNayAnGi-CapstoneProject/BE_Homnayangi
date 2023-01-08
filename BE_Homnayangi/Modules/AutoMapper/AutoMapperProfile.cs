using AutoMapper;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.DTO.RecipeDTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateCategoryMap();
        }

        private void CreateCategoryMap()
        {
            //CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<Recipe, RecipeResponse>().ReverseMap();
            CreateMap<RecipeDetail, RecipeDetailsResponse>().ReverseMap();
            CreateMap<Ingredient, IngredientResponse>().ReverseMap();
        }
    }
}
