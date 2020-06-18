using System;
using System.Net;
using System.Net.Mail;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace MessageService.Installers
{
    public class DependenciesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            // SmtpClient for sending emails
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("FaceCrack1337@gmail.com", "qhnzfvbsmefzyvds"),
            };
            services.AddSingleton(smtpClient);

            services.AddTransient<IIntegrationEventHandler<NewUserEvent>, NewUserEventHandler>();
            
            // RabbitMQ connection factory
            var rabbitHost = Environment.GetEnvironmentVariable("RABBIT_HOST") ?? throw new ArgumentNullException();
            var rabbitPort = int.Parse(Environment.GetEnvironmentVariable("RABBIT_PORT") ??
                                       throw new ArgumentNullException());
            var rabbitUser = Environment.GetEnvironmentVariable("RABBIT_USER") ?? throw new ArgumentNullException();
            var rabbitPassword = Environment.GetEnvironmentVariable("RABBIT_PASSWORD") ??
                                 throw new ArgumentNullException();
            services.AddSingleton<IEventBus>(new RabbiBus(new ConnectionFactory()
            {
                HostName = rabbitHost,
                Port = rabbitPort,
                UserName = rabbitUser,
                Password = rabbitPassword,
                DispatchConsumersAsync = true,
            }, services.BuildServiceProvider()));

        }
    }
}