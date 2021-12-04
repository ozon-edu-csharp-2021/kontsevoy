using System.Collections.Generic;
using MediatR;
using MerchandiseService.Infrastructure.Models;

namespace MerchandiseService.Infrastructure.Commands.StockApi
{
    public class StockApiGiveOutCommand : IRequest<bool>
    {
        public IReadOnlyCollection<StockItemDto> Items { get; init; }
    }
}