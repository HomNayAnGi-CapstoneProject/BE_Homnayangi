using System;

namespace BE_Homnayangi.Modules.CommentModule.Request
{
    public class CreatedCommentRequest
    {
        public Guid? ParentCommentId { get; set; }

        public Guid AuthorId { get; set; }

        public string Content { get; set; }

        public Guid BlogId { get; set; }

        public bool ByStaff { get; set; }
    }
}
