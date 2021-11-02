using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestWebservice_StaticCodeAnalysis.Security;
using System;
using System.Collections.Generic;
using System.IO;

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
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                })
                .AddXmlSerializerFormatters();

            services.AddAuthentication(BearerAuthenticationHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>(BearerAuthenticationHandler.SchemeName, null);

            services.AddControllers().ConfigureApiBehaviorOptions(o =>
            {
                o.SuppressMapClientErrors = true;
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = BearerAuthenticationHandler.SchemeName
                },
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = BearerAuthenticationHandler.SchemeName,
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
                    c.AddSecurityDefinition(BearerAuthenticationHandler.SchemeName, securityScheme);
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
