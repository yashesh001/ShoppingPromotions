namespace app.shoppingpromotions.domain
{
    public class ProductNotFoundException : Exception
    {
        private readonly string _productId;

        public ProductNotFoundException(string productId)
        {
            _productId = productId; 
        }

        public override string Message => $"Product identifier:{_productId} provided is not valid.";
    }
}
