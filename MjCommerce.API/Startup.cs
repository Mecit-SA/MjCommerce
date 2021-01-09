using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MjCommerce.Shared;
using MjCommerce.Shared.Managers.Files;
using MjCommerce.Shared.Managers.Files.Interfaces;
using MjCommerce.Shared.Managers.Identity;
using MjCommerce.Shared.Managers.Identity.Interfaces;
using MjCommerce.Shared.MapperProfiles;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Models.Identity;
using MjCommerce.Shared.Models.Orders;
using MjCommerce.Shared.Repositories;
using MjCommerce.Shared.Repositories.Interfaces;
using MjCommerce.Shared.Services.Identity;
using MjCommerce.Shared.Services.Identity.Interfaces;
using System.Text;

namespace MjCommerce.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("test", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            #region Entity Framework
            services.AddDbContext<MjCommerceDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );
            #endregion

            #region Auto Mapper
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(CountryProfile));
            services.AddAutoMapper(typeof(CityProfile));
            services.AddAutoMapper(typeof(ProductPhotoProfile));
            services.AddAutoMapper(typeof(OrderProfile));
            #endregion

            #region Repositories
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Country>, CountryRepository>();
            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<ProductPhoto>, ProductPhotoRepository>();
            #endregion

            #region Managers
            services.AddScoped<IUsersManager, UsersManager>();
            services.AddSingleton<IImagesManager, ImagesManager>();
            #endregion

            #region Identity
            services.AddScoped<IProductOwnerAuthorizationService, ProductOwnerAuthorizationService>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<MjCommerceDbContext>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });
            #endregion

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("test");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

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