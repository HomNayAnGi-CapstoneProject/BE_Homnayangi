using BE_Homnayangi.Modules.AccomplishmentModule;
using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AccomplishmentReactionModule;
using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;

using BE_Homnayangi.Modules.AutoMapper;
using BE_Homnayangi.Modules.BadgeModule;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.BlogModule;
using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule;
using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReferenceModule;
using BE_Homnayangi.Modules.BlogReferenceModule.Interface;
using BE_Homnayangi.Modules.BlogSubCateModule;
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;
using BE_Homnayangi.Modules.CategoryModule;
using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.CommentModule;
using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.CustomerBadgeModule;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerModule;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.IngredientModule;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.OrderDetailModule;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using BE_Homnayangi.Modules.OrderModule;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.PriceNoteModule;
using BE_Homnayangi.Modules.PriceNoteModule.Interface;
using BE_Homnayangi.Modules.RecipeModule;
using BE_Homnayangi.Modules.SubCateModule;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.TypeModule;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.UserModule;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Modules.VoucherModule;
using BE_Homnayangi.Modules.VoucherModule.Interface;
using BE_Homnayangi.Ultils.Quartz;
using BE_Homnayangi.Utils;
using Library.DataAccess;
using Library.Models;
using Library.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quartz;
using System;
using System.Linq;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule;
using BE_Homnayangi.Ultils.EmailServices;
using BE_Homnayangi.Modules.NotificationModule;
using BE_Homnayangi.Modules.NotificationModule.Interface;
using BE_Homnayangi.Modules.PackageModule.Interface;
using BE_Homnayangi.Modules.PackageDetailModule.Interface;
using BE_Homnayangi.Modules.PackageDetailModule;
using BE_Homnayangi.Modules.RegionModule.Interface;
using BE_Homnayangi.Modules.CookingMethodModule;
using BE_Homnayangi.Modules.CookingMethodModule.Interface;

namespace BE_Homnayangi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*  services.AddSingleton<IJobFactory, JobFactory>();
               services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
               services.AddSingleton<BadgeJob>();
               services.AddSingleton(new BadgeJobScheduler
               (
                   jobType: typeof(BadgeJob),
                   cronExpression: "0/5 * * * * ?"
               ));
               services.AddHostedService<QuartzHostedService>();*/

            services
                 .AddControllers()
                 .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                 .AddNewtonsoftJson(x => x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            services.AddSignalR();

            services.AddDbContext<HomnayangiContext>(
                 options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                // Just use the name of your job that you created in the Jobs folder.
                var jobKey = new JobKey("CheckBadgeJob");
                q.AddJob<CheckBadgeJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("CheckBadgeJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule(Configuration.GetSection("JobTime:CronSchedule").Value ?? "0/5 * * * * ?")
                );
            });
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                // Just use the name of your job that you created in the Jobs folder.
                var jobKey = new JobKey("CancelOrderJob");
                q.AddJob<CancelOrderJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("CancelOrderJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule("0/5 * * * * ?")
                );
            });
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                // Just use the name of your job that you created in the Jobs folder.
                var jobKey = new JobKey("CheckVoucherJob");
                q.AddJob<CheckVoucherJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("CheckVoucherJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule(Configuration.GetSection("JobTime:CronSchedule").Value ?? "0/5 * * * * ?")
                );
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (errorContext) =>
                {
                    var errors = errorContext.ModelState.Values.SelectMany(e => e.Errors.Select(m => new
                    {
                        ErrorMessage = m.ErrorMessage
                    })).ToList();
                    var result = new
                    {
                        Errors = errors.Select(e => e.ErrorMessage).ToList()
                    };
                    return new BadRequestObjectResult(result);
                };
            });
            #region hangfire
            var hangfireDBConnectionString = Configuration.GetConnectionString("HangfireDb");
            services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(hangfireDBConnectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));
            services.AddHangfireServer();
            #endregion
            #region Mailkit service 
            var emailConfig = Configuration
                    .GetSection("EmailConfiguration")
                    .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            #endregion
            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));
            services.Configure<AdministratorAccount>(Configuration.GetSection("AdministratorAccount"));
            var secretKey = Configuration["AppSetting:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(options =>
                 {
                     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                  .AddJwtBearer(opt =>
                  {
                      opt.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = false,
                          ValidateAudience = false,
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                          ClockSkew = TimeSpan.Zero
                      };
                  });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BE_Homnayangi", Version = "v1" });
            });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            services.Configure<RouteOptions>(r =>
            {
                r.LowercaseUrls = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed(host => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
            });
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICustomAuthorization, CustomAuthorization>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddHttpClient<OrderService>();
            // Category Module
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Blog Module
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //Ingredient Module
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IIngredientService, IngredientService>();

            //BlogTag Module
            services.AddScoped<IBlogSubCateRepository, BlogSubCateRepository>();

            //User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserService, UserService>();

            //SubCate Module
            services.AddScoped<ISubCateRepository, SubCateRepository>();
            services.AddScoped<ISubCateService, SubCateService>();

            //Voucher Module
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IVoucherService, VoucherService>();

            //CustomerVoucher Module
            services.AddScoped<ICustomerVoucherRepository, CustomerVoucherRepository>();
            services.AddScoped<ICustomerVoucherService, CustomerVoucherService>();

            //Badge Module
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IBadgeService, BadgeService>();

            //Type Module
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<ITypeService, TypeService>();

            //BlogReaction Module
            services.AddScoped<IBlogReactionRepository, BlogReactionRepository>();
            services.AddScoped<IBlogReactionService, BlogReactionService>();

            //Comment Module
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            //Blog reference Module
            services.AddScoped<IBlogReferenceRepository, BlogReferenceRepository>();

            //Accomplishment Module
            services.AddScoped<IAccomplishmentRepository, AccomplishmentRepository>();

            //Calo reference Module
            services.AddScoped<ICaloReferenceRepository, CaloReferenceRepository>();
            services.AddScoped<ICaloReferenceService, CaloReferenceService>();

            //Order Module
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            //Badge Condition Module
            services.AddScoped<IBadgeConditionRepository, BadgeConditionRepository>();
            services.AddScoped<IBadgeConditionService, BadgeConditionService>();

            //CustomerBadge Module
            services.AddScoped<ICustomerBadgeRepository, CustomerBadgeRepository>();
            services.AddScoped<ICustomerBadgeService, CustomerBadgeService>();

            //Accomplishment Module
            services.AddScoped<IAccomplishmentRepository, AccomplishmentRepository>();
            services.AddScoped<IAccomplishmentService, AccomplishmentService>();

            //PriceNote Module
            services.AddScoped<IPriceNoteRepository, PriceNoteRepository>();

            //Accomplishment Reaction Module
            services.AddScoped<IAccomplishmentReactionRepository, AccomplishmentReactionRepository>();
            services.AddScoped<IAccomplishmentReactionService, AccomplishmentReactionService>();

            //CronJob Time Config Module
            services.AddScoped<ICronJobTimeConfigService, CronJobTimeConfigService>();
            services.AddScoped<ICronJobTimeConfigRepository, CronJobTimeConfigRepository>();

            //Email sender
            services.AddScoped<IEmailSender, EmailSender>();

            //Notification Reaction Module
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            //Package Module
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageService, PackageService>();

            //PackageDetail Module
            services.AddScoped<IPackageDetailRepository, PackageDetailRepository>();
            services.AddScoped<IPackageDetailService, PackageDetailService>();

            // Region Module
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionService, RegionService>();

            // Cooking Method Module
            services.AddScoped<ICookingMethodRepository, CookingMethodRepository>();
            services.AddScoped<ICookingMethodService, CookingMethodService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BE_Homnayangi v1"));
            }

            app.UseSession();
            app.UseCors("CorsPolicy");
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRServer>("/signalRServer");
            });


        }
    }
}
