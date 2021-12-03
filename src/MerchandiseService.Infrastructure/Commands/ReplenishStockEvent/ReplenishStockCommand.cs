using System.Collections.Generic;
using MediatR;
using MerchandiseService.Infrastructure.Models;

namespace MerchandiseService.Infrastructure.Commands.ReplenishStockEvent
{
    public class ReplenishStockCommand : IRequest
    {
        public IReadOnlyCollection<StockItemDto> Items { get; init; }
    }
}