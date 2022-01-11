using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using RestWebservice_StaticCodeAnalysis.Configuration;

using RestWebService_StaticCodeAnalysis.DataAccess;
using RestWebService_StaticCodeAnalysis.DataAccess.Interfaces;
using RestWebService_StaticCodeAnalysis.ServiceAgents;
using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;
using RestWebService_StaticCodeAnalysis.Services;
using RestWebService_StaticCodeAnalysis.Services.Interfaces;

using System;
using System.Text;

using RestWebservice_StaticCodeAnalysis.Interfaces;

namespace RestWebservice_StaticCodeAnalysis
{
    /// <summary>
    /// Startup class for API configuration
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Bind configuration sections to config objects
            var jwtConfig = new JwtConfiguration();
            var sonarqubeConfig = new SonarqubeConfiguration();
            Configuration.GetSection("Jwt").Bind(jwtConfig);
            Configuration.GetSection("Sonarqube").Bind(sonarqubeConfig);
            
            // Add jwt for authentication
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtConfig.Key);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtConfig.Audience,
                        ValidIssuer = jwtConfig.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                    };
                });

            services.AddAutoMapper(typeof(Startup).Assembly);

            // Remove ASP.NET Core client erros
            services.AddControllers().ConfigureApiBehaviorOptions(o =>
            {
                o.SuppressMapClientErrors = true;
            });

            // Configure app to use netwonsoft json as (de)serializer
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                });

            // Configure EF
            services.AddDbContext<ScannerContext>(options =>
            {
                options.UseInMemoryDatabase("TemporaryInMemoryStore");
                options.UseLazyLoadingProxies();
            });

            // Configure DI
            services.AddSingleton<IJwtConfiguration>(jwtConfig);
            services.AddSingleton<ISonarqubeConfiguration>(sonarqubeConfig);

            services.AddTransient<ISonarqubeAgent, SonarqubeAgent>();
            services.AddTransient<IScanJobRepository, ScanJobRepository>();
            services.AddTransient<IScanRepository, ScanRepository>();

            services.AddTransient<IScanService, ScanService>();

            // Add jwt to swagger as authentiation scheme
            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            };

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.0.0", new OpenApiInfo
                    {
                        Version = "1.0.0",
                        Title = "Static Code Analysis API",
                        Description = "Static Code Analysis API for the Remote Compiling project",
                        Contact = new OpenApiContact()
                        {
                            Name = "Swagger Codegen Contributors",
                            Url = new Uri("https://github.com/swagger-api/swagger-codegen")
                        }
                    });
                    c.CustomSchemaIds(type => type.FullName);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            securityScheme,
                            Array.Empty<string>()
                        }
                    });
                    c.AddSecurityDefinition(securityScheme.Scheme, securityScheme);
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Static Code Analysis API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
