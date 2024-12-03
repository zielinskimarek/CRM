using CRM.Framework;

namespace CRM.Deals.Domain.Commands
{
    public sealed record DeleteDeal(Guid Id) : ICommand;
}