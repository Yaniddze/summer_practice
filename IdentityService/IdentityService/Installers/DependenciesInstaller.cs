using System.Net;
using System.Net.Mail;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.DataBase;
using TestApi.DataBase.Context;
using TestApi.DataBase.Repositories;
using TestApi.Entities;
using TestApi.Entities.User;
using TestApi.Options;
using TestApi.Repositories;
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
//            services.AddSingleton(new EntityContext());
            services.AddScoped<IContextProvider, ContextProvider>();
            services.AddScoped<IRepository<User>, UsersRepository>();

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

            services.AddScoped<IValidator<ActivateRequest>, ActivateRequestValidator>();
            services.AddScoped<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidation>();
            services.AddScoped<IValidator<RefreshTokenRequest>, RefreshRequestValidator>();
        }
    }
}