//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using FPTBlog.Src.BadgeModule;
//using FPTBlog.Src.BadgeModule.Entity;
//using FPTBlog.Src.BadgeModule.Interface;
//using FPTBlog.Src.UserModule;
//using FPTBlog.Src.UserModule.Entity;
//using FPTBlog.Src.UserModule.Interface;
//using Microsoft.Extensions.DependencyInjection;

//namespace Library.CronJob
//{
//    public class GiveBadgeJob : CronJobService
//    {
//        private readonly IServiceScopeFactory ServiceScopeFactory;
//        private readonly IBadgeService BadgeService;
//        private readonly IUserService UserService;
//        public GiveBadgeJob(IScheduleConfig<GiveBadgeJob> config, IServiceScopeFactory serviceScopeFactory)
//        : base(config.CronExpression, config.TimeZoneInfo)
//        {
//            ServiceScopeFactory = serviceScopeFactory;
//            BadgeService = (BadgeService)ServiceScopeFactory.CreateScope().ServiceProvider.GetService<IBadgeService>();
//            UserService = (UserService)ServiceScopeFactory.CreateScope().ServiceProvider.GetService<IUserService>();
//        }

//        public override Task StartAsync(CancellationToken cancellationToken)
//        {
//            Console.WriteLine("Give reward cron job starts.");
//            return base.StartAsync(cancellationToken);
//        }

//        public override Task DoWork(CancellationToken cancellationToken)
//        {
//            BadgeService.GiveBadgeJob();

//            Console.WriteLine($"{DateTime.Now:hh:mm:ss} Give reward cron job is working.");
//            return Task.CompletedTask;
//        }

//        public override Task StopAsync(CancellationToken cancellationToken)
//        {
//            Console.WriteLine("Give reward cron job is stopping.");
//            return base.StopAsync(cancellationToken);
//        }
//    }


//}
