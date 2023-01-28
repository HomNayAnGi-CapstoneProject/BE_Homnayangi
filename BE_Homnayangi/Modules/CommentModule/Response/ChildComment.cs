using System;

namespace BE_Homnayangi.Modules.CommentModule.Response
{
    public class ChildComment : ParentComment
    {
        public Guid? ParentCommentId { get; set; }
    }
}
