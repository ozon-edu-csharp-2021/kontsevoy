using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Commands.EmailService;
using MerchandiseService.Infrastructure.Commands.StockApi;
using MerchandiseService.Infrastructure.Models;
using MerchandiseService.Infrastructure.Queries.StockApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MerchandiseService.HostedServices
{
    public class ProcessorHostedService : BackgroundService
    {
        private ILogger<ProcessorHostedService> Logger { get; }
        private IServiceScopeFactory ScopeFactory { get; }

        public ProcessorHostedService(IServiceScopeFactory scopeFactory, ILogger<ProcessorHostedService> logger)
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
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var requests = await repository.FindByStatus(MerchRequestStatus.Processing, stoppingToken);
                    if (requests.Count == 0) continue;
                    var skus = await mediator.Send(new StockApiItemsQuery(), stoppingToken);
                    foreach (var merchRequest in requests)
                    {
                        try
                        {
                            var command = new StockApiGiveOutCommand
                            {
                                Items = merchRequest.MerchPack.Items.Select(f =>
                                {
                                
                                    if (!skus.Unsized.TryGetValue(f.MerchType, out var sku) 
                                        && (!skus.Sized.TryGetValue(f.MerchType, out var map) 
                                            || !map.TryGetValue(merchRequest.EmployeeClothingSize, out sku)))
                                        throw new Exception("Can't match MerchType to sku");
                                        
                                    return new StockItemDto
                                    {
                                        ItemTypeId = f.MerchType.Id,
                                        ClothingSize = merchRequest.EmployeeClothingSize.Id,
                                        Quantity = f.Quantity.Value,
                                        Sku = sku,
                                        ItemTypeName = skus.Dictionary[sku].TypeName
                                    };
                                }).ToList().AsReadOnly()
                            };
                            var now = new HandoutTimestamp(DateTime.Now);
                            var giveOut = await mediator.Send(command, stoppingToken);
                            if (giveOut)
                            {
                                var handout = JsonSerializer.Serialize<IReadOnlyCollection<StockItemDto>>(command.Items);
                                merchRequest.DoHandout(handout, now);
                                await mediator.Send(new SendEmailCommand
                                {
                                    Id = merchRequest.Id.Value,
                                    EmployeeEmail = merchRequest.EmployeeEmail,
                                    EmployeeName = merchRequest.EmployeeName,
                                    ManagerEmail = merchRequest.ManagerEmail,
                                    ManagerName = merchRequest.ManagerName,
                                    ClothingSize = merchRequest.EmployeeClothingSize.Id,
                                    MerchPackType = merchRequest.MerchPack.Id
                                }, stoppingToken);
                            }
                            else
                                merchRequest.TryHandoutNeedAwait(now);
                            await repository.UpdateAsync(merchRequest, stoppingToken);
                        }
                        catch (Exception e)
                        {
                            Logger.LogError("Error while processing request. Message {message}", e.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error while processing requests. Message {message}", ex.Message);
                }
            }
        }
    }
}