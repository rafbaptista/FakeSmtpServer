using SmtpServer;
using SmtpServer.Authentication;

namespace SmtpDemoTests.Fixtures.Email.Hooks
{
    public class SampleUserAuthenticator : UserAuthenticator, IUserAuthenticatorFactory
    {
        /// <summary>
        /// Autentica qualquer usuário e senha
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<bool> AuthenticateAsync(
            ISessionContext context,
            string user,
            string password,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public IUserAuthenticator CreateInstance(ISessionContext context)
        {
            return new SampleUserAuthenticator();
        }
    }
}
