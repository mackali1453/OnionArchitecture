using Domain.Interfaces;

namespace Domain.Entities.Aggregates
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string ProductName { get; private set; }
        public int Stock { get;private set; }

        private readonly List<PriceChange> _priceChange = new List<PriceChange>();
        public IReadOnlyCollection<PriceChange> PriceChange => _priceChange.AsReadOnly();
        public Product(string productName, int stock)
        {
            ProductName = productName;
            Stock = stock;
        }

        public Product()
        {
        }
        public void AddPrice(PriceChange price)
        {
            _priceChange.Add(price);
        }
    }
}
