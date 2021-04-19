using BusinessObject.DBModel;
using Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PaymentGateway.Context.contextManager;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.ServiceCollection
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {
            services.AddSingleton<IPaymentGateway, BusinessLogic.PaymentGateway>();
            services.AddScoped<IDataRepository<PaymentDBModel>, PaymentManager>();
            Constant._appConfig = services.BuildServiceProvider().GetService<IOptions<ConfiguraitonSettings>>();

            return services;
        }
    }
}
