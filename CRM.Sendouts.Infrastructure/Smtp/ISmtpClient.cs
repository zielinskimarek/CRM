namespace CRM.Sendouts.Infrastructure.Smtp
{
    public interface ISmtpClient
    {
        Task Send(string email);
    }
}