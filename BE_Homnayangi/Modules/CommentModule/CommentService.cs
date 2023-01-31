using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Request;
using BE_Homnayangi.Modules.CommentModule.Response;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
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

        public CommentService(ICommentRepository commentRepository,
            IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
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


                    var userComments = comments.Join(users, c => c.AuthorId, u => u.UserId, (c, u) => new ChildComment
                    {
                        CommentId = c.CommentId,
                        AuthorId = c.AuthorId.Value,
                        FullNameAuthor = u.Firstname + " " + u.Lastname,
                        Avatar = u.Avatar,
                        CreatedDate = c.CreatedDate.Value,
                        Content = c.Content,
                        Status = c.Status.Value,
                        ParentCommentId = c.ParentId != null ? c.ParentId.Value : null,
                    });
                    var customerComments = comments.Join(customers, c => c.AuthorId, cus => cus.CustomerId, (c, cus) => new ChildComment
                    {
                        CommentId = c.CommentId,
                        AuthorId = c.AuthorId.Value,
                        FullNameAuthor = cus.Firstname + " " + cus.Lastname,
                        Avatar = cus.Avatar,
                        CreatedDate = c.CreatedDate.Value,
                        Content = c.Content,
                        Status = c.Status.Value,
                        ParentCommentId = c.ParentId != null ? c.ParentId.Value : null,
                    });

                    var allComments = new List<ChildComment>();
                    allComments.AddRange(userComments);
                    allComments.AddRange(customerComments);

                    // Group by: List of parent comments order by descending created date
                    var parentComments = allComments.Where(x => x.ParentCommentId == null).OrderByDescending(x => x.CreatedDate).ToList();

                    foreach (var parent in parentComments)
                    {
                        ParentComment parentComment = new ParentComment()
                        {
                            CommentId = parent.CommentId,
                            AuthorId = parent.AuthorId,
                            FullNameAuthor = parent.FullNameAuthor,
                            CreatedDate = parent.CreatedDate,
                            Content = parent.Content,
                            Status = parent.Status,
                        };

                        List<ChildComment> childComments = new List<ChildComment>();
                        foreach (var child in allComments)
                        {
                            if (child.ParentCommentId != null && child.ParentCommentId == parent.CommentId)
                            {
                                ChildComment childComment = new ChildComment()
                                {
                                    ParentCommentId = child.ParentCommentId.Value,
                                    CommentId = child.CommentId,
                                    AuthorId = child.AuthorId,
                                    FullNameAuthor = child.FullNameAuthor,
                                    Avatar = child.Avatar,
                                    CreatedDate = child.CreatedDate,
                                    Content = child.Content,
                                    Status = child.Status,
                                };
                                childComments.Add(childComment);
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

        public async Task<ChildComment> CreateANewComment(CreatedCommentRequest comment)
        {
            ChildComment result = null;
            try
            {
                Comment newComment = new Comment()
                {
                    CommentId = Guid.NewGuid(),
                    AuthorId = comment.AuthorId,
                    CreatedDate = DateTime.Now,
                    Content = comment.Content,
                    Status = true,
                    ByStaff = comment.ByStaff,
                    ParentId = comment.ParentCommentId == null ? null : comment.ParentCommentId,
                    BlogId = comment.BlogId,
                };
                await _commentRepository.AddAsync(newComment);
                result = new ChildComment()
                {
                    CommentId = newComment.CommentId,
                    AuthorId = newComment.AuthorId.Value,
                    CreatedDate = newComment.CreatedDate.Value,
                    Content = newComment.Content,
                    Status = newComment.Status.Value,
                    ParentCommentId = newComment.ParentId != null ? newComment.ParentId : null,
                };
                if (comment.ByStaff) // get user info
                {
                    var user = await _userRepository.GetByIdAsync(comment.AuthorId);
                    result.FullNameAuthor = user.Firstname + " " + user.Lastname;
                    result.Avatar = user.Avatar;
                }
                else                // get customer info
                {
                    var customer = await _customerRepository.GetByIdAsync(comment.AuthorId);
                    result.FullNameAuthor = customer.Firstname + " " + customer.Lastname;
                    result.Avatar = customer.Avatar;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateANewComment: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<bool> DeleteAComment(Guid id)
        {
            bool result = false;
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                comment.Status = false;
                await _commentRepository.UpdateAsync(comment);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteAComment: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<bool> UpdateAComment(Guid id, string content)
        {
            bool result = false;
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                comment.Content = content.Trim();
                await _commentRepository.UpdateAsync(comment);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateAComment: " + ex.Message);
                throw;
            }
            return result;
        }
    }
}
