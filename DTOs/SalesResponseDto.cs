namespace ProductSales.DTOs
{
    public class SaleResponseDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public ClientDto Client { get; set; } = new();
        public List<SaleItemResponseDto> Items { get; set; } = new();
    }
}
