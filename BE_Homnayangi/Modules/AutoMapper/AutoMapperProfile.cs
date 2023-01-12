using AutoMapper;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.DTO.RecipeDTO;
using BE_Homnayangi.Modules.IngredientModule.Request;
using Library.Models;
using Library.Models.DTO.UserDTO;

namespace BE_Homnayangi.Modules.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateCategoryMap();
            CreateUserMap();
        }

        private void CreateCategoryMap()
        {
            //CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<Recipe, RecipeResponse>().ReverseMap();
            CreateMap<RecipeDetail, RecipeDetailsResponse>().ReverseMap();
            CreateMap<Ingredient, IngredientResponse>().ReverseMap();
            CreateMap<Ingredient, CreatedIngredientRequest>().ReverseMap();
            CreateMap<Ingredient, UpdatedIngredientRequest>().ReverseMap();
        }
        private void CreateUserMap()
        {
            CreateMap<LoginGoogleDTO, Customer>().ReverseMap();
            CreateMap<RegisterDTO, Customer>().ReverseMap();

        }


    }
}
