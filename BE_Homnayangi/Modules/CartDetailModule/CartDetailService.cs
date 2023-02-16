using BE_Homnayangi.Modules.CartDetailModule.Interface;
using BE_Homnayangi.Modules.CartDetailModule.Request;
using BE_Homnayangi.Modules.CartDetailModule.Response;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using Library.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartDetailModule
{
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;

        public CartDetailService(ICartDetailRepository cartDetailRepository,
            IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository)
        {
            _cartDetailRepository = cartDetailRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
        }

        public Task<ICollection<CartDetail>> GetCartDetailsBy(
            Expression<Func<CartDetail, bool>> filter = null,
            Func<IQueryable<CartDetail>, ICollection<CartDetail>> options = null,
            string includeProperties = null)
        {
            return _cartDetailRepository.GetCartDetailsBy(filter);
        }

        private async Task<ICollection<ItemInCart>> ToResponse(List<CartDetail> list)
        {
            if (list != null)
            {
                var result = new List<ItemInCart>();

                ICollection<Ingredient> ingredients = await _ingredientRepository.GetIngredientsBy(i => i.Status.Value);
                ICollection<Recipe> recipes = await _recipeRepository.GetRecipesBy(r => r.Status.Value == 1);
                var mappedIngredients = list.Join(ingredients, cd => cd.ItemId, i => i.IngredientId,
                    (cd, i) => new ItemInCart()
                    {
                        ItemId = cd.ItemId,
                        Name = i.Name,
                        Image = i.Picture,
                        Quantity = cd.Quantity.Value,
                        UnitPrice = cd.UnitPrice.Value
                    }
                ).ToList();

                var mappedRecipes = list.Join(recipes, cd => cd.ItemId, i => i.RecipeId,
                    (cd, i) => new ItemInCart()
                    {
                        ItemId = cd.ItemId,
                        Name = i.Title,
                        Image = i.ImageUrl,
                        Quantity = cd.Quantity.Value,
                        UnitPrice = cd.UnitPrice.Value
                    }
                ).ToList();

                if (mappedIngredients != null && mappedIngredients.Count > 0)
                {
                    result.AddRange(mappedIngredients);
                }
                if (mappedRecipes != null && mappedRecipes.Count > 0)
                {
                    result.AddRange(mappedRecipes);
                }
                return result;
            }
            return null;
        }

        public async Task<ICollection<ItemInCart>> GetCartDetailsByCartId(Guid cartId)
        {
            ICollection<ItemInCart> result = null;
            try
            {
                var cartDetails = await _cartDetailRepository.GetCartDetailsBy(c => c.CartId == cartId);
                if (cartDetails.Count() > 0)
                {
                    result = await ToResponse(cartDetails.ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCartDetailsByCartId: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<UpdatedItemInCart> UpdateQuantityItemInCart(UpdatedItemInCart updatedItemInCart)
        {
            try
            {
                if (updatedItemInCart == null || updatedItemInCart.Quantity < 0)
                {
                    return null;
                }
                CartDetail item = null;
                if (updatedItemInCart.TypeItem.Equals(CommonEnum.ItemInCart.PACKAGE_INGREDIENT)
                    || updatedItemInCart.TypeItem.Equals(CommonEnum.ItemInCart.INGREDIENT)) // isCooked = false
                {
                    item = _cartDetailRepository.GetFirstOrDefaultAsync(cd =>
                                                    cd.CartId == updatedItemInCart.CartId
                                                    && cd.ItemId == updatedItemInCart.ItemId
                                                    && !cd.IsCooked).Result;
                }
                else if (updatedItemInCart.TypeItem.Equals(CommonEnum.ItemInCart.COOKED)) // isCooked = true
                {
                    item = _cartDetailRepository.GetFirstOrDefaultAsync(cd =>
                                                    cd.CartId == updatedItemInCart.CartId
                                                    && cd.ItemId == updatedItemInCart.ItemId
                                                    && cd.IsCooked).Result;
                }

                if (item != null)
                {
                    item.Quantity = updatedItemInCart.Quantity;
                    await _cartDetailRepository.UpdateAsync(item);
                    return updatedItemInCart;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateQuantityItemInCart: " + ex.Message);
                return null;
                throw;
            }
        }

        public async Task<bool> DeleteItemInCart(Guid cartId, Guid itemId, string type)
        {
            bool isDeleted = false;
            try
            {
                var item = _cartDetailRepository.GetFirstOrDefaultAsync(
                                                    cd => cd.CartId == cartId
                                                    && cd.ItemId == itemId
                                                    && ((cd.IsCooked && type.Equals(CommonEnum.ItemInCart.COOKED)) 
                                                        || ((!cd.IsCooked && (type.Equals(CommonEnum.ItemInCart.INGREDIENT) 
                                                            || type.Equals(CommonEnum.ItemInCart.PACKAGE_INGREDIENT)))))).Result;
                if (item != null)
                {
                    await _cartDetailRepository.RemoveAsync(item);
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteItemInCart: " + ex.Message);
                throw;
            }
            return isDeleted;
        }
    }
}
