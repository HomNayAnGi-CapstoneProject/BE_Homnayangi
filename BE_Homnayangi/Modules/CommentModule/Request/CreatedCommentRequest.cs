using System;

namespace BE_Homnayangi.Modules.CommentModule.Request
{
    public class CreatedCommentRequest
    {
        public Guid? ParentCommentId { get; set; }

        public string Content { get; set; }

        public Guid BlogId { get; set; }
    }
}
