namespace CRM.Sendouts.Domain.Exceptions
{
    public sealed class InvalidSendoutStatusException(string message, Guid id) : Exception(message)
    {
        private readonly Guid _id = id;
    }
}