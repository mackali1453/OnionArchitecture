namespace Application.Dto
{
    public class ProductResponseDto : ApiResponse<List<ProductData>>
    {
        public ProductResponseDto(bool success, string message, List<ProductData> data) : base(success, message, data)
        {
        }

    }
    public class ProductData
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public List<PriceChange> PriceChange { get; set; }
        public ProductData()
        {
        }
        public ProductData(string productName, int stock, List<PriceChange> priceChange)
        {
            ProductName = productName;
            Stock = stock;
            PriceChange = priceChange;
        }


    }
    public class PriceChange
    {
        public double Price { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
