using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PaymentGateway.Context;
using PaymentGateway.ServiceCollection;

namespace PaymentGateway
{
    public class Startup
    {
        public IConfiguration configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<GzipCompressionProvider>();
            });
            services.AddControllersWithViews(ctx =>
            {
                ctx.RespectBrowserAcceptHeader = true;
            })
            .AddNewtonsoftJson(opt =>
            {
                if (opt.SerializerSettings.ContractResolver != null)
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
            });
            services.AddOptions();
            services.Configure<ConfiguraitonSettings>(configuration.GetSection("ConfiguraitonSettings"));
            ServiceCollection.AutoMapper.Configure();
            services.AddServiceDependency();
            services.AddDbContext<PaymentContext>(opts => opts.UseSqlServer(configuration.GetSection("ConfiguraitonSettings:DatabaseSettings:ConnectionString").Value));
            services.AddSwaggerDocumentation();//swagger documentation
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(cgx =>
            {
                cgx.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
