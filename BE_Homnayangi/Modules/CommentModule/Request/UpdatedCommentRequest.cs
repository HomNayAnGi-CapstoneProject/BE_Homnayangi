using System;

namespace BE_Homnayangi.Modules.CommentModule.Request
{
    public class UpdatedCommentRequest
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
    }
}
