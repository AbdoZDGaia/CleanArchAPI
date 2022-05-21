using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using NLog;
using Contracts;
using LoggerService;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Presentation.ActionFilters;

namespace JWTAuthAPI.Extensions
{
    public static class BuilderExtensions
    {
        private static readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? "https://localhost:5001";
        private static readonly string _audience = Environment.GetEnvironmentVariable("JwtAudience") ?? "https://localhost:5001";
        private static readonly string _signingKey = Environment.GetEnvironmentVariable("JwtSigningKey") ?? "SuperSecretKey@345";

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var config = builder.Configuration;

            ConfigureJson(services);
            ConfigureRepositoryManager(services);
            ConfigureServiceManager(services);
            ConfigureSqlContext(services, config);
            ConfigureControllers(services);
            ConfigureApiBehavior(services);
            AddLogging(services);
            AddSwagger(services);
            AddAuthentication(services);
            AddAutoMapper(services);
            AddFilters(services);
        }

        private static void AddFilters(IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }

        private static void ConfigureApiBehavior(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
        }

        private static void ConfigureControllers(IServiceCollection services)
        {
            NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
                new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
                .Services.BuildServiceProvider()
                .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>().First();

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            })
                .AddXmlDataContractSerializerFormatters()
                .AddCustomCSVFormatter()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
        }

        private static void ConfigureSqlContext(IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContext<RepositoryContext>(opts =>
            //{
            //    opts.UseSqlServer(config.GetConnectionString("sqlConnection"));
            //});
            services.AddSqlServer<RepositoryContext>(
                config.GetConnectionString("sqlConnection"));
        }

        private static void ConfigureServiceManager(IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        private static void ConfigureRepositoryManager(IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        private static void AddLogging(IServiceCollection services)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory()
                            , "/nlog.config"));

            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        private static void ConfigureJson(IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
            });
        }

        private static void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey))
                };
            });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT Authentication API", Version = "v1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string> { }
                    }
                });
            });
            services.AddCors();
        }
    }
}
