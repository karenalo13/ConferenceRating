using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingSystem.Application;
using RatingSystem.Application.Services;

namespace TemplateItem.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<EnrollCustomerOperation>();
            //services.AddTransient<CreateAccount>();
            //services.AddTransient<DepositMoney>();
            //services.AddTransient<WithdrawMoney>();
            //services.AddTransient<PurchaseProduct>();
            //services.AddTransient<QueryHandler>();


            //services.AddSingleton<Data.PaymentGatewayContext>();

            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var options = new ConferenceOptions
                {
                    Items = config.GetValue("ConferenceOptions:Items", 3)
                };
                return options;
            });


            return services;
        }
    }
}
