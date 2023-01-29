using BE_Homnayangi.Modules.CommentModule.Request;
using BE_Homnayangi.Modules.CommentModule.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CommentModule.Interface
{
    public interface ICommentService
    {
        public Task<List<Tuple<ParentComment, List<ChildComment>>>> GetCommentsByBlogId(Guid blogId);

        public Task<ChildComment> CreateANewComment(CreatedCommentRequest comment);

        public Task<bool> DeleteAComment(Guid id);

        public Task<bool> UpdateAComment(Guid id, string content);
    }
}
