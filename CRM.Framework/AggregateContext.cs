namespace CRM.Framework
{
    public sealed class AggregateContext<T> where T : IEntity
    {
        public T? AggregateRoot { get; private set; }
        
        public void SetAggregateRoot(T aggregateRoot)
        {
            AggregateRoot = aggregateRoot;
        }
    }
}