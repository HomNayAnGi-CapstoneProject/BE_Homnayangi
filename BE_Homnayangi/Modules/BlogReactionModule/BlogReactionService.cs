using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Response;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.NotificationModule;
using BE_Homnayangi.Modules.NotificationModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogReactionModule
{
    public class BlogReactionService : IBlogReactionService
    {
        private readonly IBlogReactionRepository _blogReactionRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public BlogReactionService(IBlogReactionRepository blogReactionRepository,
            IBlogRepository blogRepository, ICustomerRepository customerRepository,
            NotificationRepository notificationRepository,
            IHubContext<SignalRServer> hubContext)
        {
            _blogReactionRepository = blogReactionRepository;
            _blogRepository = blogRepository;
            _customerRepository = customerRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        public async Task<BlogReactionResponse> GetBlogReactionByBlogAndCustomerId(Guid blogId, Guid customerId)
        {
            BlogReaction result = null;
            try
            {
                result = await _blogReactionRepository.GetFirstOrDefaultAsync(br => br.BlogId == blogId && br.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogReactionByBlogAndCustomerId:" + ex.Message);
                throw;
            }
            return result != null ? ConvertDTO(result) : null;
        }

        private BlogReactionResponse ConvertDTO(BlogReaction br)
        {
            return new BlogReactionResponse()
            {
                BlogId = br.BlogId,
                CustomerId = br.CustomerId,
                Status = br.Status != null && br.Status.Value
            };
        }

        public async Task<BlogReactionResponse> InteractWithBlog(Guid blogId, Guid customerId)
        {
            BlogReactionResponse result = null;
            try
            {
                #region Check this blog & customer is existed in out system
                var blog = await _blogRepository.GetFirstOrDefaultAsync(b => b.BlogId == blogId && b.BlogStatus.Value == 1);
                if (blog == null) throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                var customer = await _customerRepository.GetFirstOrDefaultAsync(c => c.CustomerId == customerId && !c.IsBlocked.Value);
                if (customer == null) throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_FOUND);
                #endregion
                var reaction = await _blogReactionRepository.GetFirstOrDefaultAsync(br => br.BlogId == blogId
                                                                                    && br.CustomerId == customerId);
                if (reaction == null)
                { //  insert a new record into db
                    reaction = new BlogReaction()
                    {
                        BlogId = blogId,
                        CustomerId = customerId,
                        Status = true,
                        CreatedDate = DateTime.Now,
                    };
                    await _blogReactionRepository.AddAsync(reaction);
                    result = ConvertDTO(reaction);

                    #region Create notification
                    var notiId = Guid.NewGuid();
                    var noti = new Library.Models.Notification
                    {
                        NotificationId = notiId,
                        Description = $"{customer.Displayname ?? customer.Username} đã tương tác với bài blog [{blog.Title}] của bạn",
                        CreatedDate = DateTime.Now,
                        Status = false,
                        ReceiverId = blog.AuthorId
                    };
                    await _notificationRepository.AddAsync(noti);

                    await _hubContext.Clients.All.SendAsync($"{blog.AuthorId}_BlogReacted", JsonConvert.SerializeObject(noti));
                    #endregion
                }
                else
                { // update reaction status in db
                    reaction.Status = !reaction.Status;
                    await _blogReactionRepository.UpdateAsync(reaction);
                    result = ConvertDTO(reaction);
                }
                Blog currentBlog = await _blogRepository.GetFirstOrDefaultAsync(b => b.BlogId == blogId);
                currentBlog.Reaction = reaction.Status.Value ? ++currentBlog.Reaction : --currentBlog.Reaction;
                await _blogRepository.UpdateAsync(currentBlog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at InteractWithBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
