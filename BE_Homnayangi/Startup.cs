using System.Text.Json.Serialization;
using BE_Homnayangi.Modules.CategoryModule;
using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.IngredientModule;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.RecipeDetailModule;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.BlogModule;
using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.RecipeModule;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.AutoMapper;
using Library.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BE_Homnayangi.Modules.BlogTagModule.Interface;
using BE_Homnayangi.Modules.BlogTagModule;

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

            services.AddControllers();
            services.AddDbContext<HomnayangiContext>(
                 options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
            //Recipe Detail Module
            services.AddScoped<IRecipeDetailRepository, RecipeDetailRepository>();
            services.AddScoped<IRecipeDetailService, RecipeDetailService>();
            //Ingredient Module
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IIngredientService, IngredientService>();
            //BlogTag Module
            services.AddScoped<IBlogTagRepository, BlogTagRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BE_Homnayangi v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
