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
            public readonly static string ROLE_NOT_SUPPORTED = "This role is not valid for this action";
        }
        #endregion

        #region User error message
        public static class UserError
        {
            public readonly static string NOT_OWNER = "You are not the owner";
            public readonly static string USERNAME_EXISTED = "Username is already existed with another user";
            public readonly static string EMAIL_EXISTED = "This email is already existed with another user";
            public readonly static string PHONE_EXISTED = "This phone is already existed with another user";
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
            public readonly static string BLOG_MNG_NOT_SUPPORT = "We just support type APPROVE and REJECT";
        }
        #endregion

        #region Event error message
        public static class EventError
        {
            public readonly static string EVENT_NOT_FOUND = "Event is not found";
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
            public readonly static string QUANTITY_NOT_VALID = "Quantity must be more than 0";
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

        #region Badge error message
        public static class BadgeError
        {
            public readonly static string BADGE_NAME_EXISTED = "This badge name has existed in system";
        }
        #endregion

        #region Badge Condition error message
        public static class BadgeConditionError
        {
            public readonly static string BADGE_CONDITION_NOT_FOUND = "This badge condition can not be found";
            public readonly static string BADGE_CONDITION_IS_DUPLICATED = "This badge condition is duplicated";
        }
        #endregion
        #region CronJob Time Config error message
        public static class CronJobTimeConfigError
        {
            public readonly static string CRON_JOB_TIME_CONFIG_NOT_FOUND = "This CronJob Time Config can not be found";
            public readonly static string CRON_JOB_TIME_CONFIG_IS_DUPLICATED = "This CronJob Time Config is duplicated";
        }
        #endregion

        #region Season Reference error message
        public static class SeasonRefError
        {
            public readonly static string SEASON_REF_NOT_FOUND = "This season reference can not be found";
        }
        #endregion

        #region Manager error message
        public static class ManagerError
        {
            public readonly static string MANAGER_NOT_FOUND = "Manager is not found";
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
            public readonly static string ROLE_NOT_VALID = "Only update into Staff or Manager roles";
        }
        #endregion

        #region Comment error message
        public static class CommentError
        {
            public readonly static string COMMENT_NOT_FOUND = "This comment can not be found";
            public readonly static string NOT_THE_AUTHOR_COMMENT = "You are not the author of this comment";
        }
        #endregion

        #region General error message
        public static class GeneralError
        {
            public readonly static string SOMETHING_WENT_WRONG = "Something went wrong";
        }
        #endregion

        #region Order error message
        public static class OrderError
        {
            public readonly static string SHIPPING_DATE_NOT_VALID = "Shipping date not valid";
            public readonly static string ORDER_NOT_FOUND = "Order not found";
            public readonly static string COOKED_ORDER_NOT_VALID = "Cooked order cannot have ingredient or package";
            public readonly static string ORDER_CANNOT_CHANGE_STATUS = "Order status cannot be change";
            public readonly static string ORDER_SHIPPING_ADDRESS_REQUIRED = "Shipping address is required";
            public readonly static string ORDER_TOTAL_PRICE_NOT_VALID = "Total price not valid";
            public readonly static string ORDER_PAYMENT_METHOD_NOT_VALID = "Payment method not valid";
            public readonly static string ORDER_TOTAL_PRICE_NOT_VALID_TO_USE_VOUCHER = "Total price not valid to use voucher";
        }
        #endregion

        #region Mail service error message
        public static class MailError
        {
            public readonly static string MAIL_SENDING_ERROR = "Mail cannot be send";
        }
        #endregion

        #region Transaction error message
        public static class TransactionError
        {
            public readonly static string TRANSACTION_NOT_FOUND = "Transaction not found";
            public readonly static string TRANSACTION_NOT_ON_PENDING = "Transaction status cannot be change";
        }
        #endregion

        #region Staff error message
        public static class StaffError
        {
            public readonly static string STAFF_NOT_EXIST = "This staff does not exist";
            public readonly static string NOT_FOR_STAFF = "Staff can not be here";
        }
        #endregion

        #region Accomplishment error message
        public static class AccomplishmentError
        {
            public readonly static string ACCOMPLISHMENT_EXISTED = "This accomlishment was existed in system";
            public readonly static string ACCOMPLISHMENT_NOT_FOUND = "This accomlishment is not found";
            public readonly static string NOT_OWNER = "You are not the owner";
            public readonly static string NOT_VALID_TYPE = "Type is only APPROVE or REJECT";
            public readonly static string NOT_VALID_DATA = "You need to have either image or video";
        }
        #endregion

        #region Voucher error message
        public static class VoucherError
        {
            public readonly static string DATETIME_NOT_VALID = "Start and end date are not valid";
            public readonly static string DISCOUNT_CONDITION_NOT_VALID = "Min and max price are not valid";
            public readonly static string DISCOUNT_PRICE_NOT_VALID = "Discount is not valid";
            public readonly static string VOUCHER_NOT_AVAILABLE = "This voucher is not available";
        }
        #endregion

        #region Customer Voucher error message
        public static class CustomerVoucherError
        {
            public readonly static string CUSTOMER_VOUCHER_EXISTED = "Duplicated with Customer and Voucher data";
        }
        #endregion
    }
}

