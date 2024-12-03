namespace CRM.Sendouts.Domain.Exceptions
{
    public sealed class NoRecipientsException(string message, Guid id) : Exception(message)
    {
        private readonly Guid _id = id;
    }
}