using System;
namespace Library.Models.Constant
{
    public static class ErrorMessage
    {
        #region Common error message
        public static class CommonError
        {
            public readonly static string ID_IS_NULL = "ID request is null";
        }
        #endregion

        #region User error message
        public static class UserError
        {
            public readonly static string USER_EXISTED = "Username is already existed";
            public readonly static string USER_NOT_EXISTED = "This user can not be found";
            public readonly static string USER_NOT_LOGIN = "You did not login. Login and try this action again, please!";
            public readonly static string ACTION_FOR_USER_ROLE_ONLY = "This action is only for User role";
            public readonly static string ACTION_FOR_STAFF_AND_MANAGER_ROLE = "This action is only for Staff and Manager role";
        }
        #endregion

        #region Blog error message
        public static class BlogError
        {
            public readonly static string BLOG_NOT_FOUND = "Blog is not found";
            public readonly static string BLOG_NOT_BINDING_TO_RECIPE = "This Blog was not binding to any Recipe";
            public readonly static string BLOG_SUBCATES_LIMIT = "Max Sub categories for blog is 5";
        }
        #endregion

        #region Recipe error message
        public static class RecipeError
        {
            public readonly static string RECIPE_NOT_FOUND = "Recipe is not found";
        }
        #endregion

        #region Cart error message
        public static class CartError
        {
            public readonly static string CART_NOT_FOUND = "This cart is not found";
            public readonly static string ITEM_NOT_FOUND = "This item is not found";
            public readonly static string QUANTITY_NOT_VALID= "Quantity must be more than 0";
        }
        #endregion

        #region Customer error message
        public static class CustomerError
        {
            public readonly static string CUSTOMER_NOT_FOUND = "This customer can not be found";
            public readonly static string CUSTOMER_NOT_ALLOWED_TO_CREATE_BLOG = "Only User role is allowed to create a new blog";
            public readonly static string ACTION_FOR_CUSTOMER_ROLE_ONLY = "This action is only for Customer role";
        }
        #endregion
    }
}

