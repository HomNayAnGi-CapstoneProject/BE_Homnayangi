using BE_Homnayangi.Modules.CartDetailModule.Interface;
using BE_Homnayangi.Modules.CartDetailModule.Request;
using BE_Homnayangi.Modules.CartDetailModule.Response;
using BE_Homnayangi.Modules.CartModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartDetailModule
{
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;

        public CartDetailService(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository,
            IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository)
        {
            _cartRepository = cartRepository;
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
                if (cartDetails == null || cartDetails.Count == 0)
                {
                    throw new Exception(ErrorMessage.CartError.CART_NOT_FOUND);
                }
                else
                {
                    result = await ToResponse(cartDetails.ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCartDetailsByCartId: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<UpdatedItemInCart> UpdateQuantityItemInCart(UpdatedItemInCart updatedItemInCart)
        {
            try
            {
                if (updatedItemInCart == null || updatedItemInCart.Quantity < 0)
                {
                    throw new Exception(ErrorMessage.CartError.QUANTITY_NOT_VALID);
                }

                var item = _cartDetailRepository.GetFirstOrDefaultAsync(cd =>
                                                    cd.CartId == updatedItemInCart.CartId
                                                    && cd.ItemId == updatedItemInCart.ItemId
                                                    && cd.IsCooked == updatedItemInCart.TypeItem).Result;

                if (item == null)
                {
                    throw new Exception(ErrorMessage.CartError.ITEM_NOT_FOUND);
                }
                else
                {
                    item.Quantity = updatedItemInCart.Quantity;
                    await _cartDetailRepository.UpdateAsync(item);
                    return updatedItemInCart;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateQuantityItemInCart: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteItemInCart(Guid cartId, Guid itemId, bool type)
        {
            try
            {
                var item = _cartDetailRepository.GetFirstOrDefaultAsync(
                                                    cd => cd.CartId == cartId
                                                    && cd.ItemId == itemId
                                                    && cd.IsCooked == type).Result;
                if (item == null)
                {
                    throw new Exception(ErrorMessage.CartError.ITEM_NOT_FOUND);
                }
                else
                {
                    var cart = await _cartRepository.GetFirstOrDefaultAsync(c => c.CartId == cartId);
                    if (cart == null)
                    {
                        throw new Exception(ErrorMessage.CartError.CART_NOT_FOUND);
                    }
                    --cart.QuantityOfItem;
                    await _cartDetailRepository.RemoveAsync(item);
                    await _cartRepository.UpdateAsync(cart);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteItemInCart: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
