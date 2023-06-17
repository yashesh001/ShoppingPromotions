namespace app.shoppingpromotions.domain
{
    /// <summary>
    /// Shopping Response model
    /// </summary>
    public class ShoppingResponse
    {
        public string CustomerId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountApplied { get; set; }
        public decimal GrandTotal { get; set; }
        public int PointsEarned { get; set; }
    }
}
