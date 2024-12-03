namespace CRM.Sendouts.Infrastructure.Smtp
{
    public sealed class SmtpClient : ISmtpClient
    {
        public Task Send(string email)
        {
            Console.WriteLine($"Mail sent to {email}");
            return Task.CompletedTask;
        }
    }
}