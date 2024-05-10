using Domain.Interfaces;

namespace Domain.Entities.Aggregates
{
    public class PriceChange : BaseEntity, IAggregateRoot
    {
        public double Price { get; private set; }
        public DateTime ChangeDate { get; private set; }
        private Product _product;
        public Product Product => _product;

        public PriceChange(double price, DateTime changeDate)
        {
            Price = price;
            ChangeDate = changeDate;
        }

        public PriceChange()
        {
        }
    }
}
