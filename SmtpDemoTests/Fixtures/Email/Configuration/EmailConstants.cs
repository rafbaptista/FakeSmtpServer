namespace SmtpDemoTests.Fixtures.Email.Configuration
{
    public static class EmailConstants
    {
        public readonly static string SentName = "email-sent.csv";

        public readonly static string SavedPath = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.Parent.FullName, SentName);
    }
}
