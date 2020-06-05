using MediatR;

namespace TestApi.UseCases.SendMail
{
    public class EmailRequest: IRequest<EmailAnswer>
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}