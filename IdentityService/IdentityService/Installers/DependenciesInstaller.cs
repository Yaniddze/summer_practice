using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApi.DataBase;
using TestApi.DataBase.Repositories;
using TestApi.Entities;
using TestApi.Repositories;

namespace TestApi.Installers
{
    public class DependenciesInstaller : IInstaller
    {
        public void installServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new Context());
            services.AddScoped<IRepository<User>, UsersRepository>();

            var smtpClient = new SmtpClient("smtp.gmail.ru", 587)
            {
                Credentials = new NetworkCredential("YaniddzeMail@gmail.com", "qhnzfvbsmefzyvds"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
            };
            services.AddSingleton(smtpClient);
        }
    }
}