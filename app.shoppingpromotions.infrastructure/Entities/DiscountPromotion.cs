namespace app.shoppingpromotions.infrastructure.Entities
{
    public class DiscountPromotion
    {
        public string DiscountPromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
    }

}