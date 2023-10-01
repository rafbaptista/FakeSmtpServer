using MailKit.Net.Smtp;
using MimeKit;

namespace SmtpDemo
{
    public class MailService
    {
        public void SendEmail()
        {
            Console.WriteLine("Iniciando envio de email");

            var email = GetMessage();

            using (var client = new SmtpClient())
            {
                client.Connect(Environment.GetEnvironmentVariable("SMTP_SERVER"), Convert.ToInt32(Environment.GetEnvironmentVariable("SMTP_PORT")), false);
                client.Authenticate(Environment.GetEnvironmentVariable("SMTP_USERNAME"), Environment.GetEnvironmentVariable("SMTP_PASSWORD"));
                client.Send(email);
                client.Disconnect(true);
            }
            Console.WriteLine("Email enviado com sucesso");
        }

        private MimeMessage GetMessage()
        {
            var bodyBuilder = new BodyBuilder
            {
                TextBody = "Corpo do email Teste"
            };

            var message = new MimeMessage
            {
                Sender = new MailboxAddress("Teste", Environment.GetEnvironmentVariable("SMTP_SENDER")),
                Subject = "Email teste",
                Body = bodyBuilder.ToMessageBody()
            };

            message.To.Add(MailboxAddress.Parse("rafabap100@gmail.com"));
            return message;
        }
    }
}
