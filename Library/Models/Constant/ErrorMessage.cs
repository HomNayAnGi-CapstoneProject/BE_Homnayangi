using System;
namespace Library.Models.Constant
{
    public static class ErrorMessage
    {
        #region Common error message
        public static class CommonError
        {
            public readonly static string ID_IS_NULL = "ID request is null";
            public readonly static string LIST_IS_NULL = "List have no data";
            public readonly static string INVALID_REQUEST = "Request data is invalid";
            public readonly static string NOT_LOGIN = "Customer did not login";
        }
        #endregion

        #region User error message
        public static class UserError
        {
            public readonly static string NOT_OWNER = "You are not the owner";
            public readonly static string USERNAME_EXISTED = "Username is already existed with another user";
            public readonly static string EMAIL_EXISTED = "This email is already existed with another user";
            public readonly static string USER_NOT_EXISTED = "This user can not be found";
            public readonly static string USER_EXISTED = "This user is existed";
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
            public readonly static string NOT_OWNER = "You are not the owner";
            public readonly static string CUSTOMER_NOT_FOUND = "This customer can not be found";
            public readonly static string USERNAME_EXISTED = "This username has existed with another customer";
            public readonly static string EMAIL_EXISTED = "This email has existed with another customer";
            public readonly static string CUSTOMER_NOT_ALLOWED_TO_CREATE_BLOG = "Staff and Manager role are allowed to create a new blog";
            public readonly static string CUSTOMER_NOT_ALLOWED_TO_DELETE_BLOG = "Staff and Manager role are allowed to delete a blog";
            public readonly static string CUSTOMER_NOT_ALLOWED_TO_DELETE_RECIPE = "Staff and Manager role are allowed to delete a recipe";
            public readonly static string CUSTOMER_NOT_ALLOWED_TO_RESTORE_BLOG = "Staff and Manager role are allowed to restore an old blog";
            public readonly static string CUSTOMER_NOT_ALLOWED_TO_RESTORE_RECIPE = "Staff and Manager role are allowed to restore an old reipce";
            public readonly static string ACTION_FOR_CUSTOMER_ROLE_ONLY = "This action is only for Customer role";
        }
        #endregion

        #region Calo Reference error message
        public static class CaloRefError
        {
            public readonly static string CALO_REF_NOT_FOUND = "This calo reference can not be found";
        }
        #endregion

        #region Badge Condition error message
        public static class BadgeConditionError
        {
            public readonly static string BADGE_CONDITION_NOT_FOUND = "This badge condition can not be found";
        }
        #endregion

        #region Season Reference error message
        public static class SeasonRefError
        {
            public readonly static string SEASON_REF_NOT_FOUND = "This season reference can not be found";
        }
        #endregion

        #region Admin error message
        public static class AdminError
        {
            public readonly static string ADMIN_ONLY = "Only admin can do this function";
            public readonly static string ADMIN_NOT_ALLOWED_TO_DELETE_BLOG = "Staff and Manager role are allowed to delete a blog";
            public readonly static string ADMIN_NOT_ALLOWED_TO_DELETE_RECIPE = "Staff and Manager role are allowed to delete a recipe";
            public readonly static string ADMIN_NOT_ALLOWED_TO_RESTORE_BLOG = "Staff and Manager role are allowed to restore an old blog";
            public readonly static string ADMIN_NOT_ALLOWED_TO_RESTORE_RECIPE = "Staff and Manager role are allowed to restore an old reipce";
        }
        #endregion

        #region Comment error message
        public static class CommentError
        {
            public readonly static string COMMENT_NOT_FOUND = "This comment can not be found";
            public readonly static string NOT_THE_AUTHOR_COMMENT = "You are not the author of this comment";
        }
        #endregion

        #region Staff error message
        public static class StaffError
        {
            public readonly static string STAFF_NOT_EXIST = "This staff does not exist";
        }
        #endregion
    }
}

