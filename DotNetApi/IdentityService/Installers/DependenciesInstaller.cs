using System;
using EventBus;
using EventBus.Abstractions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Commands.Abstractions;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Queries.Abstractions;
using TestApi.DataBase;
using TestApi.DataBase.CQRS.Commands;
using TestApi.DataBase.CQRS.Queries;
using TestApi.Entities.Tokens;
using TestApi.Entities.Users;
using TestApi.Options;
using TestApi.UseCases.ActivateAccount;
using TestApi.UseCases.Login;
using TestApi.UseCases.RefreshToken;
using TestApi.UseCases.Registration;

namespace TestApi.Installers
{
    public class DependenciesInstaller : IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new ContextProvider(
                Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                    ?? throw new ArgumentNullException()
            ));

            // Valid emails, that mapped from appsettings.json
            var validEmails = new ValidEmails();
            configuration.Bind(nameof(ValidEmails), validEmails);
            services.AddSingleton(validEmails);
            
            // Valid platforms, that mapped from appsettings.json
            var validPlatforms = new ValidPlatforms();
            configuration.Bind(nameof(ValidPlatforms), validPlatforms);
            services.AddSingleton(validPlatforms);

            // Validators
            services.AddTransient<IValidator<ActivateRequest>, ActivateRequestValidator>();
            services.AddTransient<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidation>();
            services.AddTransient<IValidator<RefreshTokenRequest>, RefreshRequestValidator>();

            // CQRS
            services.AddTransient<IQueryHandler<AuthQuery, User>, AuthQueryHandler>();
            services.AddTransient<IQueryHandler<UserByEmailQuery, User>, UserByEmailHandler>();
            services.AddTransient<IQueryHandler<UserByLoginQuery, User>, UserByLoginHandler>();
            services.AddTransient<IQueryHandler<TokenQuery, Token>, TokenQueryHandler>();

            services.AddTransient<ICommandHandler<AddUserCommand>, AddUserCommandHandler>();
            services.AddTransient<ICommandHandler<ConfirmEmailCommand>, ConfirmEmailCommandHandler>();
            services.AddTransient<ICommandHandler<WriteTokenCommand>, WriteTokenCommandHandler>();
            
            // RabbitMQ connection factory
            var rabbitHost = Environment.GetEnvironmentVariable("RABBIT_HOST") 
                             ?? throw new ArgumentNullException();
            var rabbitPort = int.Parse(Environment.GetEnvironmentVariable("RABBIT_PORT") 
                                       ?? throw new ArgumentNullException());
            var rabbitUser = Environment.GetEnvironmentVariable("RABBIT_USER") 
                             ?? throw new ArgumentNullException();
            var rabbitPassword = Environment.GetEnvironmentVariable("RABBIT_PASSWORD") 
                                 ?? throw new ArgumentNullException();
            services.AddSingleton<IEventBus>(new RabbiBus(new ConnectionFactory
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