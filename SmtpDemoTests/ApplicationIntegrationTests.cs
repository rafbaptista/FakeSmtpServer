using SmtpDemoTests.Fixtures.Email;
using SmtpDemoTests.Extensions;
using SmtpDemo;
using MimeKit;

namespace SmtpDemoTests
{
    public class ApplicationIntegrationTests
    {
        private readonly EmailFixture _emailFixture;

        public ApplicationIntegrationTests()
        {
            _emailFixture = new EmailFixture();
        }

        [Fact]
        public async Task GivenEmailIsSent_WhenApplicationIsExecuted_MustSendWithCorrectValues()
        {
            //Act
            Program.Main(null);
            MimeMessage email = _emailFixture.GetSentEmail();

            //Assert
            AssertExtensions.EmailWasSent();
            AssertExtensions.EmailToEqual("rafabap100@gmail.com", email.To[0].Name);
            AssertExtensions.EmailSubjectEqual("Email teste", email.Subject);
            AssertExtensions.EmailBodyEqual("Corpo do email Teste", email.TextBody);
        }
    }
}