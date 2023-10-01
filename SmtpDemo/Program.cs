namespace SmtpDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var emailService = new MailService();
            emailService.SendEmail();
        }
    }
}