namespace app.shoppingpromotions.domain
{
    public class ResponseCartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
