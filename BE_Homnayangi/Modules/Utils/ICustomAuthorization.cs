using BE_Homnayangi.Modules.UserModule.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.Utils
{
    public interface ICustomAuthorization
    {
        public CurrentUserResponse loginUser();
        public void Login(CurrentUserResponse user);

    }
}
