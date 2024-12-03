namespace CRM.Framework
{
    public interface ICommandsQueue
    {
        void Queue(ICommand command);
        bool Dequeue(out ICommand? command);
    }
    
    public sealed class CommandsQueue : ICommandsQueue
    {
        private readonly Queue<ICommand> _queue = new();

        public void Queue(ICommand command)
        {
            _queue.Enqueue(command);
        }

        public bool Dequeue(out ICommand? command)
        {
            if (_queue.Any())
            {
                command = _queue.Dequeue();
                return true;
            }

            command = null;
            return false;
        }
    }
}