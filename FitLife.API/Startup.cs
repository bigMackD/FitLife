using System;
using System.Text;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Authentication;
using FitLife.Contracts.Response.Product;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.Infrastructure.CommandHandlers;
using FitLife.Infrastructure.CommandHandlers.Authentication;
using FitLife.Infrastructure.QueryHandlers.Products;
using FitLife.Infrastructure.QueryHandlers.Users;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace FitLife.API
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
            services
                .AddScoped<IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse>,
                    RegisterUserCommandHandler>()
                .AddScoped<IAsyncCommandHandler<LoginUserCommand, LoginUserResponse>,
                    LoginUserCommandHandler>() 
                .AddScoped<IAsyncQueryHandler<GetUsersQuery, GetUsersResponse>,
                    GetUsersQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>,
                    GetUserProfileQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse>,
                    UserDetailsQueryHandler>()
                .AddScoped<IQueryHandler<GetProductsQuery, GetProductsResponse>,
                    GetProductsQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse>,
                GetProductDetailsQueryHandler>();

            services.AddControllers();
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddDbContext<FoodContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options =>
                options.Password.RequiredLength = 8
            );


            //JWT Authentication

            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:JWTSecret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins(Configuration.GetValue<string>("AppSettings:ClientUrl"))
                    .AllowAnyHeader()
                    .AllowAnyMethod());

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
