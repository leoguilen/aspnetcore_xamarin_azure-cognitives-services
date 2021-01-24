using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartAuth.Configurations;
using SmartAuth.Interfaces;
using SmartAuth.Repositories;
using SmartAuth.Services;
using System;
using System.IO;
using System.Reflection;

namespace SmartAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddApiVersioning(options => 
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.Configure<DatabaseConfiguration>(config => config.ConnectionString = Configuration.GetConnectionString("DefaultConnection"));
            services.Configure<ComputerVisionConfiguration>(Configuration.GetSection(nameof(ComputerVisionConfiguration)));

            services.AddScoped(typeof(IRepository<>), typeof(DapperRepository<>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IComputerVisionService, ComputerVisionService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartAuth API",
                    Version = "v1",
                    Description = "Serviço de cadastro inteligente de usuários",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger(options => options.RouteTemplate = "{documentName}/swagger.json");

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.SwaggerEndpoint("v1/swagger.json", "SmartAuth API");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
