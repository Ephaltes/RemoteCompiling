using System.Text;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.PipelineBehavior;
using RestWebservice_RemoteCompiling.Repositories;

using Serilog;

namespace RestWebservice_RemoteCompiling
{
    public class Startup
    {
        private readonly string _AllAllowedPolicy = "AllAllowedPolicy";

        public IConfiguration Configuration
        {
            get;
            private set;
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddOptions();
            services.AddSingleton<IPistonHelper, PistonHelper>();
            services.AddSingleton<IAliasHelper, AliasHelper>();
            services.AddSingleton<IHttpHelper>(x => new HttpHelper
                                                   (Configuration.GetSection("RemoteCompilerApiLocation").Value));
            services.AddTransient<ILdapHelper, LdapHelper>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IExerciseGradeRepository, ExerciseGradeRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();


            string connectionString = Configuration.GetConnectionString("Database");
            services.AddDbContext<RemoteCompileDbContext>(options => options
                                                              .UseLazyLoadingProxies()
                                                              .UseNpgsql(connectionString)
                , ServiceLifetime.Scoped);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                              {
                                  options.TokenValidationParameters = new TokenValidationParameters
                                                                      {
                                                                          ValidateIssuer = true,
                                                                          ValidateAudience = true,
                                                                          ValidateLifetime = true,
                                                                          ValidateIssuerSigningKey = true,
                                                                          ValidIssuer = Configuration["Jwt:Issuer"],
                                                                          ValidAudience = Configuration["Jwt:Audience"],
                                                                          IssuerSigningKey = new
                                                                              SymmetricSecurityKey
                                                                              (Encoding.UTF8.GetBytes
                                                                                  (Configuration["Jwt:Key"]))
                                                                      };
                              });

            services.AddAuthorization(options =>
                                      {
                                          AuthorizationPolicyBuilder? defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                                              JwtBearerDefaults.AuthenticationScheme);

                                          defaultAuthorizationPolicyBuilder =
                                              defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                                          options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                                      });

            services.AddCors(options =>
                             {
                                 options.AddPolicy(_AllAllowedPolicy,
                                     builder =>
                                     {
                                         builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                                         //.WithMethods("POST", "GET");
                                     });
                             });


            services.AddControllers();
            services.AddSwaggerGen(options =>
                                   {
                                       options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestWebservice_RemoteCompiling", Version = "v1" });
                                       options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                               {
                                                                                   Name = "Authorization",
                                                                                   Type = SecuritySchemeType.ApiKey,
                                                                                   Scheme = "Bearer",
                                                                                   BearerFormat = "JWT",
                                                                                   In = ParameterLocation.Header,
                                                                                   Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                                                                               });
                                       options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                      {
                                                                          {
                                                                              new OpenApiSecurityScheme
                                                                              {
                                                                                  Reference = new OpenApiReference
                                                                                              {
                                                                                                  Type = ReferenceType.SecurityScheme,
                                                                                                  Id = "Bearer"
                                                                                              }
                                                                              },
                                                                              new string[] { }
                                                                          }
                                                                      });
                                   });

            //CQRS Pattern + Mediator Pattern https://www.youtube.com/watch?v=YzOBrVlthMk&feature=youtu.be
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestWebservice_RemoteCompiling v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllers();
                             });

            // app.Use(async (context, next) =>
            //         {
            //             var token = context.Session.GetString("Token");
            //             if (!string.IsNullOrEmpty(token))
            //             {
            //                 context.Request.Headers.Add("Authorization", "Bearer " + token);
            //             }
            //             await next();
            //         });
        }
    }
}