using MimeKit;
using SmtpDemoTests.Fixtures.Email.Hooks;
using SmtpServer.ComponentModel;
using SmtpServer;

namespace SmtpDemoTests.Fixtures.Email
{
    public class InMemoryEmailFixture
    {
        public InMemoryEmailFixture()
        {
            Initialize();
        }
        public void Initialize()
        {
            SetEnvironmentVariables();

            var options = CreateOptions();
            var serviceProvider = BuildServiceProvider();
            var server = new SmtpServer.SmtpServer(options, serviceProvider);
            var serverTask = server.StartAsync(CancellationToken.None);
        }

        public MimeMessage GetSentEmail()
        {
            return InMemoryMessageStore.SentMessage;
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
            serviceProvider.Add(new InMemoryMessageStore());
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
    }
}
