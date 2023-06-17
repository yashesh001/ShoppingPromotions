namespace app.shoppingpromotions.infrastructure.Entities
{
    public class PointsPromotion
    {
        public string PointsPromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public decimal PointsPerDollarSpent { get; set; }
    }
}