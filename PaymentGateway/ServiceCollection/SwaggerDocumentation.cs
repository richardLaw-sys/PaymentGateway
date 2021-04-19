using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.ServiceCollection
{
    public static class SwaggerDocumentation
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(swag =>
            {
                swag.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Documentation",
                    Description = "ASP.NET Core 3.1 - Payment",
                    Contact = new OpenApiContact
                    {
                        Name = "Suhel Ahmed",
                        Email = "ahmedsuhel13@gmail.com",
                        Url = new Uri("https://www.facebook.com/suhelahmed20"),
                    }
                });
            });
        }
    }
}
