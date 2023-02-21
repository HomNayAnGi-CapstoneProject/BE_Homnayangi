using BE_Homnayangi.Modules.UserModule.Response;
using Microsoft.AspNetCore.Http;

namespace BE_Homnayangi.Utils
{
    public static class CustomAuthorization
    {
        public static CurrentUserResponse loginUser
        {
            get
            {
                IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
                return (_httpContextAccessor.HttpContext.Session.GetString("LoginUser") != null) ?
                            JsonUtils.DeserializeComplexData<CurrentUserResponse>(_httpContextAccessor.HttpContext.Session.GetString("LoginUser")) : null;
            }
        }
        public static void Login(CurrentUserResponse user)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetString("LoginUser", JsonUtils.SerializeComplexData(user));
        }
    }
}
