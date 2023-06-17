using System.Text.Json.Serialization;

namespace app.shoppingpromotions.domain
{
    public class ResponseCartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [JsonIgnore]
        public decimal ItemDiscount { get; set; }
        public decimal DiscountedPrice
        {
            get
            {
                return (Price - ItemDiscount) < 0 ? 0 : Price - ItemDiscount;
            }
        }
        public int Quantity { get; set; }

        [JsonIgnore]
        public decimal TotalAmount
        {
            get { return Price * Quantity; }
        }

        [JsonIgnore]
        public decimal TotalDiscount
        {
            get { return (Price - DiscountedPrice) * Quantity; }
        }
    }
}
