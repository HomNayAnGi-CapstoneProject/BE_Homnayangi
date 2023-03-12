using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Request;
using BE_Homnayangi.Modules.CommentModule.Response;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Response;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CommentModule
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBlogRepository _blogRepository;

        public CommentService(ICommentRepository commentRepository, IBlogRepository blogRepository,
            IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _blogRepository = blogRepository;
        }

        public async Task<List<Tuple<ParentComment, List<ChildComment>>>> GetCommentsByBlogId(Guid blogId)
        {
            ICollection<Comment> comments = null;
            List<Tuple<ParentComment, List<ChildComment>>> result = null;
            try
            {
                comments = await _commentRepository.GetCommentsBy(x => x.BlogId == blogId && x.Status != null && x.Status.Value);

                if (comments != null && comments.Count > 0)
                {
                    result = new List<Tuple<ParentComment, List<ChildComment>>>();
                    var users = await _userRepository.GetUsersBy(u => u.IsBlocked == null || !u.IsBlocked.Value);
                    var customers = await _customerRepository.GetCustomersBy(c => c.IsBlocked == null || !c.IsBlocked.Value);

                    var userComments = comments.Join(users, c => c.AuthorId, u => u.UserId,
                        (c, u) => new ChildComment()
                        {
                            ParentCommentId = c.ParentId,
                            CommentId = c.CommentId,
                            AuthorId = c.AuthorId.Value,
                            FullNameAuthor = u.Firstname + " " + u.Lastname,
                            Avatar = u.Avatar,
                            CreatedDate = c.CreatedDate.Value,
                            Content = c.Content,
                            Status = c.Status.Value,
                        });
                    var customerComments = comments.Join(customers, com => com.AuthorId, cus => cus.CustomerId, (com, cus)
                        => new ChildComment()
                        {
                            ParentCommentId = com.ParentId,
                            CommentId = com.CommentId,
                            AuthorId = com.AuthorId.Value,
                            FullNameAuthor = cus.Firstname + " " + cus.Lastname,
                            Avatar = cus.Avatar,
                            CreatedDate = com.CreatedDate.Value,
                            Content = com.Content,
                            Status = com.Status.Value,
                        });

                    var allComments = new List<ChildComment>();
                    allComments.AddRange(userComments);
                    allComments.AddRange(customerComments);

                    // Group by: List of parent comments order by descending created date
                    var parentComments = allComments.Where(x => x.ParentCommentId == null).OrderByDescending(x => x.CreatedDate).ToList();

                    foreach (var parent in parentComments)
                    {
                        ParentComment parentComment = parent;
                        List<ChildComment> childComments = new List<ChildComment>();
                        foreach (var child in allComments)
                        {
                            if (child.ParentCommentId != null && child.ParentCommentId == parent.CommentId)
                            {
                                childComments.Add(child);
                            }
                        }
                        var tmp = Tuple.Create(parentComment, childComments);
                        result.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCommentsByBlogId: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<ChildComment> CreateANewComment(CreatedCommentRequest comment, CurrentUserResponse user)
        {
            ChildComment result = null;
            try
            {
                var blog = await _blogRepository.GetByIdAsync(comment.BlogId);
                if (blog == null) throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                Comment newComment = new Comment()
                {
                    CommentId = Guid.NewGuid(),
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now,
                    Content = comment.Content,
                    Status = true,
                    ParentId = comment.ParentCommentId == null ? null : comment.ParentCommentId,
                    BlogId = comment.BlogId,
                    ByStaff = !user.Role.Equals("Customer"),
                };
                await _commentRepository.AddAsync(newComment);

                result = new ChildComment()
                {
                    ParentCommentId = newComment.ParentId != null ? newComment.ParentId : null,
                    CommentId = newComment.CommentId,
                    AuthorId = newComment.AuthorId.Value,
                    CreatedDate = newComment.CreatedDate.Value,
                    Content = newComment.Content,
                    Status = newComment.Status.Value,
                    FullNameAuthor = user.Firstname + " " + user.Lastname,
                    Avatar = user.Avatar
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateANewComment: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<bool> DeleteAComment(Guid id, Guid userID)
        {
            bool result = false;
            try
            {
                var comment = await _commentRepository.GetFirstOrDefaultAsync(c => c.CommentId == id && c.Status.Value);
                if (comment == null) throw new Exception(ErrorMessage.CommentError.COMMENT_NOT_FOUND);
                if (comment.AuthorId == userID)
                {
                    comment.Status = false;
                    await _commentRepository.UpdateAsync(comment);
                    result = true;
                }
                else
                {
                    throw new Exception(ErrorMessage.CommentError.NOT_THE_AUTHOR_COMMENT);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteAComment: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> UpdateAComment(Guid id, string content, Guid userID)
        {
            bool result = false;
            try
            {
                var comment = await _commentRepository.GetFirstOrDefaultAsync(c => c.CommentId == id && c.Status.Value);
                if (comment == null) throw new Exception(ErrorMessage.CommentError.COMMENT_NOT_FOUND);
                if (comment.AuthorId == userID)
                {
                    comment.Content = content.Trim();
                    await _commentRepository.UpdateAsync(comment);
                    result = true;
                }
                else
                {
                    throw new Exception(ErrorMessage.CommentError.NOT_THE_AUTHOR_COMMENT);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateAComment: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
