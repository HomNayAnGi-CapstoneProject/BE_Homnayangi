using System;

namespace BE_Homnayangi.Modules.CommentModule.Response
{
    public class ParentComment
    {
        public Guid CommentId { get; set; }

        public Guid AuthorId { get; set; } // is used for authenticating author with update or delete action

        public string FullNameAuthor { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Content { get; set; }

        public bool Status { get; set; }

        public bool ByStaff { get; set; }

        public override string ToString() {
            return $"{CommentId}|{AuthorId}|{FullNameAuthor}|{Avatar}|{CreatedDate}|{Content}|{Status}|{ByStaff}";
        }
    }
}
