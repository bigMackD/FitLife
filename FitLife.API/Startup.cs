using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using FitLife.API.Helpers;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.Contracts.Request.Command.Products;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.Contracts.Request.Query.MealCategories;
using FitLife.Contracts.Request.Query.Meals;
using FitLife.Contracts.Request.Query.Products;
using FitLife.Contracts.Request.Query.UserMeals;
using FitLife.Contracts.Request.Query.Users;
using FitLife.Contracts.Response.Authentication;
using FitLife.Contracts.Response.MealCategories;
using FitLife.Contracts.Response.Meals;
using FitLife.Contracts.Response.Product;
using FitLife.Contracts.Response.UserMeals;
using FitLife.Contracts.Response.Users;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.Infrastructure.CommandHandlers.Authentication;
using FitLife.Infrastructure.CommandHandlers.Meals;
using FitLife.Infrastructure.CommandHandlers.Products;
using FitLife.Infrastructure.CommandHandlers.UserMeals;
using FitLife.Infrastructure.QueryHandlers.MealCategories;
using FitLife.Infrastructure.QueryHandlers.Meals;
using FitLife.Infrastructure.QueryHandlers.Products;
using FitLife.Infrastructure.QueryHandlers.UserMeals;
using FitLife.Infrastructure.QueryHandlers.Users;
using FitLife.Infrastructure.Validators.Meals;
using FitLife.Infrastructure.Validators.Products;
using FitLife.Shared.Infrastructure.CommandHandler;
using FitLife.Shared.Infrastructure.QueryHandler;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FitLife.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionService.Set(configuration);
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
                .AddScoped<IAsyncCommandHandler<DisableUserCommand, DisableUserResponse>,
                    DisableUserCommandHandler>()
                .AddScoped<IAsyncCommandHandler<EnableUserCommand, EnableUserResponse>,
                    EnableUserCommandHandler>()
                .AddScoped<IAsyncQueryHandler<GetUsersQuery, GetUsersResponse>,
                    GetUsersQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>,
                    GetUserProfileQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse>,
                    UserDetailsQueryHandler>()
                .AddScoped<IQueryHandler<GetProductsQuery, GetProductsResponse>,
                    GetProductsQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetProductDetailsQuery, GetProductDetailsResponse>,
                    GetProductDetailsQueryHandler>()
                .AddScoped<IAsyncCommandHandler<AddProductCommand, AddProductResponse>,
                    AddProductCommandHandler>()
                .AddScoped<IAsyncCommandHandler<AddMealCommand, AddMealResponse>,
                    AddMealCommandHandler>()
                .AddScoped<IQueryHandler<GetMealCategoriesQuery, GetMealCategoriesResponse>,
                    GetMealCategoriesQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetMealsQuery, GetMealsResponse>,
                    GetMealsQueryHandler>()
                .AddScoped<IAsyncCommandHandler<EditProductCommand, EditProductResponse>,
                    EditProductCommandHandler>()
                .AddScoped<IAsyncQueryHandler<GetMealDetailsQuery, GetMealDetailsResponse>,
                    GetMealDetailsQueryHandler>()
                .AddScoped<IAsyncCommandHandler<EditMealCommand, EditMealResponse>,
                    EditMealCommandHandler>()
                .AddScoped<IAsyncCommandHandler<DeleteMealCommand, DeleteMealResponse>,
                    DeleteMealCommandHandler>()
                .AddScoped<IAsyncCommandHandler<AddUserMealCommand, AddUserMealResponse>,
                    AddUserMealCommandHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserMealsByDateInternalQuery, GetUserMealsByDateResponse>,
                    GetUserMealsByDateQueryHandler>()
                .AddScoped<IAsyncCommandHandler<DeleteUserMealsCommand, DeleteUserMealsReponse>,
                    DeleteUserMealsCommandHandler>()


                .AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>()
                .AddScoped<IValidator<GetProductsQuery>, GetProductsQueryValidator>()
                .AddScoped<IValidator<AddMealCommand>, AddMealCommandValidator>()
                .AddScoped<IValidator<EditProductCommand>, EditProductCommandValidator>()
                .AddScoped<IValidator<EditMealCommand>, EditMealCommandValidator>();

            //TODO: Register all handlers
            //var commandHandlers = typeof(LoginUserCommandHandler).Assembly.GetTypes()
            //    .Where(t => t.GetInterfaces()
            //        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncCommandHandler<,>))
            //    ).ToList();

            //foreach (var handler in commandHandlers)
            //{
            //    var genericArgs = handler.GetInterfaces().First(i =>
            //        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncCommandHandler<,>)).GetGenericArguments();

            //    services.AddScoped(handler<,>, handler);
            //}

            services.AddControllers();
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(ConnectionService.connectionString));
            services.AddDbContext<FoodContext>(options =>
                options.UseSqlServer(ConnectionService.connectionString));
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FitLife",
                    Description = "FitLife app API",
                    Contact = new OpenApiContact
                    {
                        Name = "Maciej Drozdowicz",
                        Email = "maciek.d@me.com",
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <BR/>
                      Enter 'Bearer' [space] and then your token in the text input below.  <BR/>
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitLife API v.1");
                c.RoutePrefix = string.Empty;
            });
        }

        
    }
}
