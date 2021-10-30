using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.PipelineBehavior;
using Serilog;

namespace RestWebservice_RemoteCompiling
{
    public class Startup
    {
        private readonly string _AllAllowedPolicy = "AllAllowedPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IPistonHelper, PistonHelper>();
            services.AddSingleton<IAliasHelper, AliasHelper>();
            services.AddSingleton<IHttpHelper>(x => new HttpHelper
            (Configuration.GetSection("RemoteCompilerApiLocation").Value));
            services.AddSingleton<ILdapHelper, LdapHelper>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _AllAllowedPolicy,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                                      //.WithMethods("POST", "GET");
                                  });
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestWebservice_RemoteCompiling", Version = "v1" });
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
        }
    }
}
