using System;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.HostedServices
{
    public class AcceptorHostedService : BackgroundService
    {
        private ILogger<AcceptorHostedService> Logger { get; }
        private IServiceScopeFactory ScopeFactory { get; }

        public AcceptorHostedService(IServiceScopeFactory scopeFactory, ILogger<AcceptorHostedService> logger)
        {
            ScopeFactory = scopeFactory;
            Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = ScopeFactory.CreateScope();
                try
                {
                    await Task.Delay(1000, stoppingToken);
                    var repository = scope.ServiceProvider.GetRequiredService<IMerchRequestRepository>();
                    var requests = await repository.FindByStatus(MerchRequestStatus.New, stoppingToken);
                    foreach (var merchRequest in requests)
                    {
                        try
                        {
                            merchRequest.ReadyToProcessing();
                            await repository.UpdateAsync(merchRequest, stoppingToken);
                        }
                        catch (Exception e)
                        {
                            Logger.LogError("Error while change request status. Message {message}", e.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error while get accept requests. Message {message}", ex.Message);
                }
            }
        }
    }
}