namespace app.shoppingpromotions.domain
{
    /// <summary>
    /// Shopping request model
    /// </summary>
    public class ShoppingRequest
    {
        public string CustomerId { get; set; } = string.Empty;
        public string LoyaltyCard { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public List<BasketItem> Basket { get; set; } = new List<BasketItem>();
    }
}
