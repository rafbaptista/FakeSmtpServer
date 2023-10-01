using MimeKit;
using SmtpDemo;
using SmtpDemoTests.Fixtures.Email;

namespace SmtpDemoTests.Tests
{
    public class InMemoryEmailIntegrationTests
    {
        private readonly InMemoryEmailFixture _inMemoryEmailFixture;

        public InMemoryEmailIntegrationTests()
        {
            _inMemoryEmailFixture = new InMemoryEmailFixture();
        }

        [Fact]
        public async Task GivenEmailIsSent_WhenApplicationIsExecuted_ThenMustCheckIfEmailWasPersistedOnMemory()
        {
            //Act
            Program.Main(null);
            MimeMessage email = _inMemoryEmailFixture.GetSentEmail();

            //Assert
            Assert.NotNull(email);
            Assert.Equal("rafabap100@gmail.com", email.To[0].ToString());
            Assert.Equal("Email teste", email.Subject);
            Assert.Equal("Corpo do email Teste", email.TextBody);
        }
    }
}
