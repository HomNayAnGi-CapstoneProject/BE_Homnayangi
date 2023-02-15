using System;
namespace Library.Models.Constant
{
    public static class ErrorMessage
    {
        #region User error message
        public static class UserError
        {
            public readonly static string USER_EXISTED = "Username is already existed";
        }
        #endregion

        #region Blog error message
        public static class BlogError
        {
            public readonly static string BLOG_NOT_FOUND = "Blog not found";
            public readonly static string BLOG_NOT_BINDING_TO_RECIPE = "This Blog was not binding to any Recipe";
            public readonly static string BLOG_SUBCATES_LIMIT = "Max Sub categories for blog is 5";
        }
        #endregion
    }
}

