using System;

namespace BE_Homnayangi.Modules.BlogReactionModule.Response
{
    public class BlogReactionResponse
    {
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public bool? Status { get; set; }
    }
}
