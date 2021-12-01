using System.Collections.Generic;
using MediatR;
using MerchandiseService.Infrastructure.Models;

namespace MerchandiseService.Infrastructure.Commands.ReplenishStock
{
    public class ReplenishStockCommand : IRequest
    {
        public IReadOnlyCollection<StockItemDto> Items { get; init; }
    }
}