using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TestApi.UseCases.SendMail
{
    public class SendEmailUseCase: IRequestHandler<EmailRequest, EmailAnswer>
    {
        private readonly SmtpClient _client;

        public SendEmailUseCase(SmtpClient client)
        {
            _client = client;
        }

        public async Task<EmailAnswer> Handle(EmailRequest request, CancellationToken cancellationToken)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("YaniddzeMail@gmail.com"),
                Subject = request.Subject,
                Body = request.Message,
            };
            
            mail.To.Add(new MailAddress(request.Email));

            try
            {
                await _client.SendMailAsync(mail);
                return new EmailAnswer{Success = true};
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return new EmailAnswer{Success = false};
            }
        }
    }
}
