namespace app.shoppingpromotions.domain
{
    public class BasketItem
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
