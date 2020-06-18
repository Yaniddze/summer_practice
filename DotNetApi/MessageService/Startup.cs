using System;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace MessageService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IIntegrationEventHandler<MainIntegrationEvent>, MainEventHandler>();
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            var bus = app.ApplicationServices.GetService<IEventBus>();
            bus.Subscribe<MainEventHandler, MainIntegrationEvent>(
                nameof(MainIntegrationEvent),
                env.ApplicationName
            );


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}