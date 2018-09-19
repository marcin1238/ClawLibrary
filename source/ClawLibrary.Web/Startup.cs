using System.IO;
using System.Text;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Extensions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Core.Middlewares;
using ClawLibrary.Core.Models.Auth;
using ClawLibrary.Core.Services;
using ClawLibrary.Data.DataServices;
using ClawLibrary.Data.Mapping;
using ClawLibrary.Data.Models;
using ClawLibrary.Mail;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Mapping;
using ClawLibrary.Services.Validation.Books;
using ClawLibrary.Services.Validation.Users;
using ClawLibrary.Web.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace ClawLibrary.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private static readonly string secretKey = "mysupersecret_secretkey!123";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        private readonly string _appUrl;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            env.ConfigureNLog("nlog.config");
            _appUrl = Configuration.GetValue<string>("AppUrl");
        }

        /// <summary>
        /// Configuration root
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddMvcOptions(options => options.Filters.Add(new ValidateInputFilter()))
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddFluentValidation(fv => fv
                    .RegisterValidatorsFromAssemblyContaining<BookRequestValidator>()
                    .RegisterValidatorsFromAssemblyContaining<RegisterUserRequestValidator>());

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RegularRole", policy => policy.RequireRole("Regular"));
            });

            // Add application services.
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataMappingProfile());
                cfg.AddProfile(new MappingProfile());
            })));

            services.AddSingleton<IAuthDataService, AuthDataService>();
            services.AddSingleton<IAuthApiService, AuthApiService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IUsersDataService, UsersDataService>();
            services.AddSingleton<IUsersApiService, UsersApiService>();
            services.AddSingleton<IBooksDataService, BooksDataService>();
            services.AddSingleton<IBooksApiService, BooksApiService>();

            services.AddSingleton<TokenProviderOptions>(new TokenProviderOptions
            {
                Audience = _appUrl,
                Issuer = _appUrl,
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256),
            });
            services.AddSingleton<IMediaStorageAppService>(new FileSystemMediaStorageAppService
            {
                RootPath = Configuration["MediaStorageRootPath"]
            });
            services.AddSingleton(new AuthConfig());
            services.AddSingleton<ISessionContextProvider, PerRequestSessionContextProvider>();
            services.AddSingleton(Configuration.GetSection("MailConfig").Get<MailConfig>());
            services.AddSingleton<IMailDataService, MailDataService>();
            services.AddSingleton<IMailGenerator, MailGenerator>();
            services.AddSingleton<IMailSender, MailSender>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Claw Library",
                    Description = "A Claw Library API",
                    TermsOfService = "None",
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ClawLibrary.Web.xml");
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                c.OperationFilter<DefaultResponsesOperationFilter>();
            });

            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("default")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) => {
                await next();
//                if (context.Response.StatusCode == 404 &&
//                    !Path.HasExtension(context.Request.Path.Value) &&
//                    !context.Request.Path.Value.StartsWith("/api/"))
//                {
//                    context.Request.Path = "/index.html";
//                    await next();
//                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseClawLibraryAuth();

            // Exception handling middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<SessionContextMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Claw Library API V1");
            });

            app.UseMvcWithDefaultRoute();
            
        }
    }
}
