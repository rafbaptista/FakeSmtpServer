using SmtpServer.ComponentModel;
using SmtpServer;
using MimeKit;
using SmtpDemoTests.Fixtures.Email.Hooks;
using SmtpDemoTests.Fixtures.Email.Configuration;

namespace SmtpDemoTests.Fixtures.Email
{
    public class EmailFixture
    {
        public EmailFixture()
        {
            Initialize();
        }
        public void Initialize()
        {
            DeleteRemainingEmail();
            SetEnvironmentVariables();

            var options = CreateOptions();
            var serviceProvider = BuildServiceProvider();
            var server = new SmtpServer.SmtpServer(options, serviceProvider);
            var serverTask = server.StartAsync(CancellationToken.None);
        }

        public MimeMessage GetSentEmail()
        {
            string[] content = File.
                ReadAllText(EmailConstants.SavedPath)
                .Split(Environment.NewLine)[1] //skip header
                .Split(';');

            var bodyBuilder = new BodyBuilder
            {
                TextBody = content[2]
            };

            var message = new MimeMessage
            {
                Subject = content[1],
                Body = bodyBuilder.ToMessageBody()
            };

            message.To.Add(MailboxAddress.Parse(content[0]));

            return message;
        }

        private ISmtpServerOptions CreateOptions()
        {
            return new SmtpServerOptionsBuilder()
                .ServerName("localhost")
                .Endpoint(endpointBuilder => endpointBuilder
                    .Port(587)
                    .IsSecure(false)
                    .AllowUnsecureAuthentication(true)
                    .AuthenticationRequired(true)
                    .Build())
                .Build();
        }

        private ServiceProvider BuildServiceProvider()
        {
            var serviceProvider = new ServiceProvider();
            serviceProvider.Add(new SampleMessageStore());
            serviceProvider.Add(userAuthenticatorFactory: new SampleUserAuthenticator());
            serviceProvider.Add(userAuthenticator: new SampleUserAuthenticator());
            return serviceProvider;
        }

        private void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("SMTP_SERVER", "localhost");
            Environment.SetEnvironmentVariable("SMTP_PORT", "587");
            Environment.SetEnvironmentVariable("SMTP_USERNAME", "user");
            Environment.SetEnvironmentVariable("SMTP_PASSWORD", "password");
            Environment.SetEnvironmentVariable("SMTP_SENDER", "rafabap100@gmail.com");            
        }

        private void DeleteRemainingEmail()
        {
            File.Delete(EmailConstants.SavedPath);
        }
    }
}
