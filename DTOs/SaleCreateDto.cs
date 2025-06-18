namespace ProductSales.DTOs
{
    public class SaleCreateDto
    {
        public int ClientId { get; set; }
        public List<SaleItemCreateDto>? Items { get; set; }
    }

    public class SaleItemCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
