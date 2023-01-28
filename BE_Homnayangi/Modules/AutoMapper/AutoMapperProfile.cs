using AutoMapper;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.DTO.RecipeDTO;
using BE_Homnayangi.Modules.IngredientModule.Request;
using BE_Homnayangi.Modules.TypeModule.DTO;
using BE_Homnayangi.Modules.UserModule.Request;
using BE_Homnayangi.Modules.VoucherModule.Request;
using BE_Homnayangi.Modules.VoucherModule.Response;
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

            // Recipe
            CreateMap<Recipe, RecipeResponse>().ReverseMap();

            // RecipeDetail
            CreateMap<RecipeDetail, RecipeDetailsResponse>().ReverseMap();

            // Ingredient
            CreateMap<Ingredient, IngredientResponse>().ReverseMap();
            CreateMap<Ingredient, CreatedIngredientRequest>().ReverseMap();
            CreateMap<Ingredient, UpdatedIngredientRequest>().ReverseMap();

            // Voucher
            CreateMap<Voucher, CreateVoucherRequest>().ReverseMap();
            CreateMap<Voucher, UpdateVoucherRequest>().ReverseMap();

            // CustomerVoucher
            CreateMap<CustomerVoucher, CreatedCustomerVoucherRequest>().ReverseMap();

            // Type
            CreateMap<Type, CreateTypeRequest>().ReverseMap();
            CreateMap<Type, UpdateTypeRequest>().ReverseMap();
        }
        private void CreateUserMap()
        {
            CreateMap<LoginGoogleDTO, Customer>().ReverseMap();
            CreateMap<RegisterDTO, Customer>().ReverseMap();
            CreateMap<UpdateCustomer, Customer>().ReverseMap();
            CreateMap<UpdateUser, User>().ReverseMap();
            // CreateMap<UpdateUserProfile, User>().ReverseMap();
            CreateMap<CreateUser, User>().ReverseMap();
        }


    }
}
