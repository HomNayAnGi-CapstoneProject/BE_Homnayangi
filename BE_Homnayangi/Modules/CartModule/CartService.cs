using BE_Homnayangi.Modules.CartDetailModule.Interface;
using BE_Homnayangi.Modules.CartDetailModule.Request;
using BE_Homnayangi.Modules.CartModule.Interface;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartModule
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICustomerRepository _customerRepository;

        public CartService(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository,
            IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository,
            ICustomerRepository customerRepository)
        {
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _customerRepository = customerRepository;
        }

        public Task<ICollection<Cart>> GetCartsBy(
            Expression<Func<Cart, bool>> filter = null,
            Func<IQueryable<Cart>, ICollection<Cart>> options = null,
            string includeProperties = null)
        {
            return _cartRepository.GetCartsBy(filter);
        }

        public Cart GetCartByCustomerId(Guid customerId)
        {
            Cart result = null;
            try
            {
                CheckCustomerIsExisted(customerId);
                result = _cartRepository.GetFirstOrDefaultAsync(c => c.CustomerId == customerId).Result;
                if (result == null)
                {
                    throw new Exception(ErrorMessage.CartError.CART_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCartByCustomerId: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<Cart> InsertNewItemIntoCart(InsertedItemIntoCart newItem)
        {
            Cart cart = null;
            try
            {
                CheckCustomerIsExisted(newItem.CustomerId);
                await CheckItemIsExisted(newItem.ItemId, newItem.TypeItem);
                cart = CheckCartExistedByCustomerId(newItem.CustomerId);

                // No exist cart with this customerId
                if (cart == null)
                {
                    // Init new cart for this customerId
                    cart = new Cart()
                    {
                        CartId = Guid.NewGuid(),
                        CustomerId = newItem.CustomerId,
                        QuantityOfItem = 1
                    };
                    await _cartRepository.AddAsync(cart);

                    // Insert new item into cart detail
                    await _cartDetailRepository.AddAsync(await InitCartDetail(cart.CartId, newItem.ItemId, newItem.TypeItem));
                }
                // existed cart with this customerid
                else
                {
                    ICollection<CartDetail> cartDetails = await _cartDetailRepository.GetCartDetailsBy(cd => cd.CartId == cart.CartId);
                    bool isExisted = false;
                    foreach (var cd in cartDetails)
                    {
                        if (cd.ItemId == newItem.ItemId && cd.IsCooked == newItem.TypeItem)
                        {
                            isExisted = true;
                            ++cd.Quantity;
                            await _cartDetailRepository.UpdateAsync(cd);
                            break;
                        }
                    }
                    if (!isExisted)
                    {
                        // Insert new item into cart detail
                        await _cartDetailRepository.AddAsync(await InitCartDetail(cart.CartId, newItem.ItemId, newItem.TypeItem));
                        ++cart.QuantityOfItem;
                        await _cartRepository.UpdateAsync(cart);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at InsertNewItemIntoCart: " + ex.Message);
                throw;
            }
            return cart;
        }

        private async Task CheckItemIsExisted(Guid itemId, bool type)
        {
            try
            {
                if (type) // cooked > recipe
                {
                    var item = await _recipeRepository.GetByIdAsync(itemId);
                    if (item == null)
                    {
                        throw new Exception(ErrorMessage.CartError.ITEM_NOT_FOUND);
                    }
                }
                else
                {
                    var recipe = await _recipeRepository.GetByIdAsync(itemId);
                    if (recipe == null)
                    {
                        var ingredient = await _ingredientRepository.GetByIdAsync(itemId);
                        if (ingredient == null)
                        {
                            throw new Exception(ErrorMessage.CartError.ITEM_NOT_FOUND);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CheckItemIsExisted: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void CheckCustomerIsExisted(Guid customerId)
        {
            try
            {
                var customer = _customerRepository.GetFirstOrDefaultAsync(c => c.CustomerId == customerId).Result;
                if (customer == null)
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CheckCustomerIsExisted: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private Cart CheckCartExistedByCustomerId(Guid customerId)
        {
            try
            {
                return _cartRepository.GetFirstOrDefaultAsync(c => c.CustomerId == customerId).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CheckCartExistedByCustomerId: " + ex.Message);
                throw;
            }
        }

        public async Task<CartDetail> InitCartDetail(Guid cartId, Guid itemId, bool typeItem)
        {
            CartDetail cartDetail = new CartDetail()
            {
                CartId = cartId,
                ItemId = itemId,
                Quantity = 1
            };

            // Get price based on type item
            if (typeItem) // cooked
            {
                var cookedItem = await _recipeRepository.GetByIdAsync(itemId);
                cartDetail.UnitPrice = cookedItem.CookedPrice;
                cartDetail.IsCooked = true;
            }
            else
            {

                var ingredient = await _ingredientRepository.GetByIdAsync(itemId);
                if (ingredient != null) // ingredient
                {
                    cartDetail.UnitPrice = ingredient.Price;
                    cartDetail.IsCooked = false;
                }
                else // PackagePrice
                {
                    var packageIngredient = await _recipeRepository.GetByIdAsync(itemId);
                    cartDetail.UnitPrice = packageIngredient.PackagePrice;
                    cartDetail.IsCooked = false;
                }
            }
            return cartDetail;
        }

    }
}
