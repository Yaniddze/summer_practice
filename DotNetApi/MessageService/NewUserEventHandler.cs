using System;
using System.Net.Mail;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;

namespace MessageService
{
    public class NewUserEventHandler: IIntegrationEventHandler<NewUserEvent>
    {
        private readonly SmtpClient _client;

        public NewUserEventHandler(SmtpClient client)
        {
            _client = client;
        }

        public async Task HandleAsync(NewUserEvent @event)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("FaceCrack1337@gmail.com"),
                Subject = "Подтверждение регистрации",
                Body = $"Подтвердите аккаунт: https://yaniddze.com/activate/{@event.ActivationUrl}",
            };
            
            mail.To.Add(new MailAddress(@event.Email));

            try
            {
                await _client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}