using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public CommentService(ICommentRepository commentRepository, IMapper mapper,
            IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
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

        public async Task<bool> CreateANewComment(CreatedCommentRequest comment)
        {
            ChildComment result = null;
            try
            {
                Comment newComment = new Comment()
                {
                    ParentId = comment.ParentCommentId == null ? null : comment.ParentCommentId,
                    AuthorId = comment.AuthorId,
                    Content = comment.Content,
                    BlogId = comment.BlogId,
                    ByStaff = comment.ByStaff,
                    CommentId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Status = true,
                };
                //await _commentRepository.AddAsync(newComment);
                //if (comment.ByStaff) // get user info
                //{
                //    var authorNavigation = await _userRepository.GetByIdAsync(comment.AuthorId);
                //    newComment.AuthorNavigation = authorNavigation;
                //}
                //else                // get customer info
                //{
                //    var author = await _customerRepository.GetByIdAsync(comment.AuthorId);
                //    newComment.Author = author;
                //}
                //result = new ChildComment()
                //{
                //    ParentCommentId = newComment.ParentId != null ? newComment.ParentId : null,
                //    CommentId = newComment.CommentId,
                //    AuthorId = newComment.AuthorId.Value,
                //    FullNameAuthor = newComment.Author != null
                //                        ? newComment.Author.Firstname + " " + newComment.Author.Lastname
                //                        : newComment.AuthorNavigation.Firstname + " " + newComment.AuthorNavigation.Lastname,
                //    Avatar = newComment.Author != null ? newComment.Author.Avatar : newComment.AuthorNavigation.Avatar,
                //    CreatedDate = newComment.CreatedDate.Value,
                //    Content = newComment.Content,
                //    Status = newComment.Status.Value,
                //};
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
