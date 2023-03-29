using AutoMapper;
using BE_Homnayangi.Modules.CommentModule.Response;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.DTO.RecipeDTO;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.Request;
using BE_Homnayangi.Modules.OrderModule.Request;
using BE_Homnayangi.Modules.OrderModule.Response;
using BE_Homnayangi.Modules.RewardModule.DTO.Request;
using BE_Homnayangi.Modules.TypeModule.DTO;
using BE_Homnayangi.Modules.UserModule.Request;
using BE_Homnayangi.Modules.VoucherModule.Request;
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
            CreateMap<IngredientRequest, CreatedIngredientRequest>().ReverseMap();
            CreateMap<Ingredient, CreatedIngredientRequest>().ReverseMap();
            CreateMap<Ingredient, UpdatedIngredientRequest>().ReverseMap();
            CreateMap<IngredientRequest, UpdatedIngredientRequest>().ReverseMap();

            // Voucher
            CreateMap<Voucher, CreateVoucherRequest>().ReverseMap();
            CreateMap<Voucher, UpdateVoucherRequest>().ReverseMap();

            // CustomerVoucher
            CreateMap<CustomerVoucher, CreatedCustomerVoucherRequest>().ReverseMap();

            // Type
            CreateMap<Type, CreateTypeRequest>().ReverseMap();
            CreateMap<Type, UpdateTypeRequest>().ReverseMap();

            // Comment
            CreateMap<Comment, ParentComment>().ReverseMap();
            CreateMap<ParentComment, Comment>().ReverseMap();
            CreateMap<ChildComment, Comment>().ReverseMap();

            // Order
            CreateMap<Order, CreateOrderRequest>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderDetail, OrderResponse.OrderDetailResponse>().ReverseMap();
            // Badge
            CreateMap<CreateNewBadgeRequest, Badge>().ReverseMap();
            CreateMap<UpdateBadgeRequest, Badge>().ReverseMap();
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
