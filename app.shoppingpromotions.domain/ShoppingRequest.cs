namespace app.shoppingpromotions.domain
{
    /// <summary>
    /// Shopping request model
    /// </summary>
    public class ShoppingRequest
    {
        public string CustomerId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
    }
}
