using SmtpDemoTests.Fixtures.Email.Configuration;

namespace SmtpDemoTests.Extensions
{
    public class AssertExtensions : Xunit.Assert
    {
        public static void EmailWasSent()
        {
            True(File.Exists(EmailConstants.SavedPath));
        }

        private static void EmailEqual(string expected, string actual, int position, bool ignoreCase = true)
        {
            string[] content = File.
                ReadAllText(EmailConstants.SavedPath)
                .Split(Environment.NewLine)[1] //skip header
                .Split(';');

            string value = content[position];

            Equal(expected, value, ignoreCase);
        }

        public static void EmailToEqual(string expected, string actual, bool ignoreCase = true)
        {
            EmailEqual(expected, actual, 0, ignoreCase);
        }

        public static void EmailSubjectEqual(string expected, string actual, bool ignoreCase = true)
        {
            EmailEqual(expected, actual, 1, ignoreCase);
        }

        public static void EmailBodyEqual(string expected, string actual, bool ignoreCase = true)
        {
            EmailEqual(expected, actual, 2, ignoreCase);
        }
    }
}
