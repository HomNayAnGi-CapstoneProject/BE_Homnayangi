using BE_Homnayangi.Modules.AccomplishmentModule;
using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AccomplishmentReactionModule;
using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule;
using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface;
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
using BE_Homnayangi.Modules.RecipeDetailModule;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.SubCateModule;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.TransactionModule;
using BE_Homnayangi.Modules.TransactionModule.Interface;
using BE_Homnayangi.Modules.TypeModule;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.UnitModule;
using BE_Homnayangi.Modules.UnitModule.Interface;
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
            /*   services.AddSingleton<IJobFactory, JobFactory>();
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

            services.AddDbContext<HomnayangiContext>(
                 options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddQuartz(opt =>
            {
                opt.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("BadgeJob");
                opt.AddJob<BadgeJob>(option => option.WithIdentity(jobKey));
                opt.AddTrigger(opts => opts
               .ForJob(jobKey)
               .WithIdentity($"{jobKey}-trigger")
               .WithCronSchedule(Configuration.GetSection("BadgeJob:CronSchedule").Value ?? "0 0/5 0 ? * * *"));
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

            // Category Module
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Blog Module
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();

            // Recipe Module
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeService, RecipeService>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //Recipe Detail Module
            services.AddScoped<IRecipeDetailRepository, RecipeDetailRepository>();
            services.AddScoped<IRecipeDetailService, RecipeDetailService>();

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

            //Unit Module
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IUnitService, UnitService>();

            //Blog reference Module
            services.AddScoped<IBlogReferenceRepository, BlogReferenceRepository>();

            //Accomplishment Module
            services.AddScoped<IAccomplishmentRepository, AccomplishmentRepository>();

            //Calo reference Module
            services.AddScoped<ICaloReferenceRepository, CaloReferenceRepository>();
            services.AddScoped<ICaloReferenceService, CaloReferenceService>();

            //Season reference Module
            services.AddScoped<ISeasonReferenceRepository, SeasonReferenceRepository>();
            services.AddScoped<ISeasonReferenceService, SeasonReferenceService>();

            //Order Module
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            //Transaction Module
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
