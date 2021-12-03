namespace MerchandiseService.Infrastructure.Models
{
    public class StockItemDto
    {
        public long Sku { get; init; }
        public int ItemTypeId { get; init; }
        public string ItemTypeName { get; init; }
        public int? ClothingSize { get; init; }
        public int Quantity { get; init; }
    }
}