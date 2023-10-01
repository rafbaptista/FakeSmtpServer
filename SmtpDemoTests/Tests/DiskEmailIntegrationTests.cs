using SmtpDemoTests.Fixtures.Email;
using SmtpDemoTests.Extensions;
using SmtpDemo;
using MimeKit;

namespace SmtpDemoTests.Tests
{
    public class DiskEmailIntegrationTests
    {
        private readonly DiskEmailFixture _diskEmailFixture;

        public DiskEmailIntegrationTests()
        {
            _diskEmailFixture = new DiskEmailFixture();
        }

        [Fact]
        public async Task GivenEmailIsSent_WhenApplicationIsExecuted_ThenMustCheckIfEmailWasPersistedOnDisk()
        {
            //Act
            Program.Main(null);
            MimeMessage email = _diskEmailFixture.GetSentEmail();

            //Assert
            AssertExtensions.EmailWasSent();
            AssertExtensions.EmailToEqual("rafabap100@gmail.com", email.To[0].ToString());
            AssertExtensions.EmailSubjectEqual("Email teste", email.Subject);
            AssertExtensions.EmailBodyEqual("Corpo do email Teste", email.TextBody);
        }
    }
}