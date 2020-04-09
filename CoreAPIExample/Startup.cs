using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CoreAPIExample.Middleware;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoreAPIExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc(opt => opt.EnableEndpointRouting = false);

            // Add Swagger UI
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "CoreAPIExample",
                    Description = "An example .NEdAPI to liven up my github account"
                });
            });

            // Add BL services to DI Container
            services.AddScoped<IEmployeeService, EmployeeService>();

            // Add DA to DI Container
            services.AddSingleton<IEmployeeDA, EmployeeDA>();

            // init local db
            services.AddSingleton<ConnectionStrings>();

            services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            }
            else
            {
                app.UseCors();
            }

            // add swagger for viewing available API methods
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreAPIExample v1" + (env.IsDevelopment() ? "[DEV]" : string.Empty));
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc();
            
            // Custom middleware to catch and log errors
            app.UseExceptionHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
