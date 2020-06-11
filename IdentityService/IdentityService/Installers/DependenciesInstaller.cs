using System.Net;
using System.Net.Mail;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.CQRS.Commands;
using TestApi.CQRS.Queries;
using TestApi.DataBase.Context;
using TestApi.DataBase.CQRS.Users.Commands.Add;
using TestApi.DataBase.CQRS.Users.Commands.Update.ConfirmEmail;
using TestApi.DataBase.CQRS.Users.Commands.Update.WriteToken;
using TestApi.DataBase.CQRS.Users.Queries;
using TestApi.Entities.User;
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
            services.AddScoped<IContextProvider, ContextProvider>();

            // SmtpClient for sending emails
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("FaceCrack1337@gmail.com", "qhnzfvbsmefzyvds"),
            };
            services.AddSingleton(smtpClient);

            // Valid emails, that mapped from appsettings.json
            var validEmails = new ValidEmails();
            configuration.Bind(nameof(ValidEmails), validEmails);
            services.AddSingleton(validEmails);

            // Validators
            services.AddScoped<IValidator<ActivateRequest>, ActivateRequestValidator>();
            services.AddScoped<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidation>();
            services.AddScoped<IValidator<RefreshTokenRequest>, RefreshRequestValidator>();

            // CQRS
            services.AddScoped<IFindQuery<User>, FindUserQuery>();
            services.AddScoped<IGetByIdQuery<User>, GetUserById>();
            services.AddScoped<ICommandHandler<AddUserCommand>, AddUserCommandHandler>();
            services.AddScoped<ICommandHandler<ConfirmEmailCommand>, ConfirmEmailCommandHandler>();
            services.AddScoped<ICommandHandler<WriteTokenCommand>, WriteTokenCommandHandler>();
        }
    }
}