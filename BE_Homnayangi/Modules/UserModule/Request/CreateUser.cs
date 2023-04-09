using System.ComponentModel.DataAnnotations;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class CreateUser : CreateManager
    {
        [Required(ErrorMessage = "Displayname is required")]
        public string Displayname { get; set; }
    }
}
