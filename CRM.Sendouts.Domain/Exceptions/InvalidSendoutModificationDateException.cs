using CRM.Sendouts.Domain.Aggregate;

namespace CRM.Sendouts.Domain.Exceptions
{
    public sealed class InvalidSendoutModificationDateException(string message, Guid id) : Exception(message)
    {
        private readonly Guid _id = id;
    }
}