using CRM.Deals.Domain;
using CRM.Framework;
using MediatR;

namespace CRM.Deals.Api
{
    public class DealsHostedService(
        ICommandsQueue commandsQueue,
        IServiceScopeFactory serviceScopeFactory)
        : IHostedService, IDisposable
    {
        private Timer? _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private async void DoWork(object? _)
        {
            if (commandsQueue.Dequeue(out var command))
            {
                try
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    var @event = await mediator!.Send(command!);

                    var dealsRepository = scope.ServiceProvider.GetService<IDealsRepository>();
                    await dealsRepository!.SaveAsync(@event, CancellationToken.None);
                    await mediator.Publish(@event);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}