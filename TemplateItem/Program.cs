using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingSystem.Application.Queries;
using RatingSystem.Application.Services;
using RatingSystem.Data;
using RatingSystem.ExternalService;
using RatingSystem.PublishedLanguage.Commands;
using RatingSystem.PublishedLanguage.Events;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TemplateItem.Application;
using TemplateItem.Data;

namespace RatingSystem
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // setup
            var services = new ServiceCollection();

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            services.RegisterBusinessServices(Configuration);
            services.AddPaymentDataAccess(Configuration);

            services.Scan(scan => scan
                .FromAssemblyOf<ListOfRatings>()
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());


            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));

            services.AddScopedContravariant<INotificationHandler<INotification>, AllEventsHandler>(typeof(RatingMade).Assembly);

            services.AddMediatR(new[] { typeof(ListOfRatings).Assembly, typeof(AllEventsHandler).Assembly }); // get all IRequestHandler and INotificationHandler classes

            services.AddSingleton(Configuration);

            // build
            var serviceProvider = services.BuildServiceProvider();
            var database = serviceProvider.GetRequiredService<PaymentGatewayContext>();
            var ibanService = serviceProvider.GetRequiredService<NewIban>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();


            //var makeAccountDetails = new MakeNewRating
            //{
            //    UniqueIdentifier = "23",
            //    AccountType = "Debit",
            //    Valuta = "Eur"
            //};


            //await mediator.Send(makeAccountDetails, cancellationToken);



            //var query = new Application.Queries.ListOfRatings.Query
            //{
            //    DateTime = DateTime.UtcNow
            //};

            //var result = await mediator.Send(query, cancellationToken);


        }
    }
}