using BE_Homnayangi.Modules.UserModule.Response;
using BE_Homnayangi.Modules.Utils;
using Microsoft.AspNetCore.Http;

namespace BE_Homnayangi.Utils
{
    public class CustomAuthorization : ICustomAuthorization
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CustomAuthorization(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public CurrentUserResponse loginUser()
        {
            return (_httpContextAccessor.HttpContext.Session.GetString("LoginUser") != null) ?
                        JsonUtils.DeserializeComplexData<CurrentUserResponse>(_httpContextAccessor.HttpContext.Session.GetString("LoginUser")) : null;

        }

        public void Login(CurrentUserResponse user)
        {
            _httpContextAccessor.HttpContext.Session.SetString("LoginUser", JsonUtils.SerializeComplexData(user));
        }
    }
}
