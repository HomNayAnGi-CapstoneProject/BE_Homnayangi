using BE_Homnayangi.Modules.AccomplishmentModule.Request;
using System;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Response
{
    public class DetailAccomplishment : UpdatedAccomplishment
    {
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public string AuthorImage{ get; set; }
        public string AuthorFullName { get; set; }
        public string? VerifierFullName { get; set; }
    }
}
