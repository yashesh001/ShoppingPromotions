using app.shoppingpromotions.domain;
using FluentValidation;

namespace app.shoppingpromotions.host.Validators
{
    public class ShoppingRequestValidator : AbstractValidator<ShoppingRequest>
    {
        public ShoppingRequestValidator()
        {
            RuleFor(dto => dto.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(dto => dto.LoyaltyCard).NotEmpty().WithMessage("Loyalty card is required.");
            RuleFor(dto => dto.TransactionDate).NotEmpty().WithMessage("Transaction date is required.");

            RuleFor(dto => dto.Basket).NotNull().NotEmpty().WithMessage("Basket items are required.");
            RuleForEach(dto => dto.Basket).SetValidator(new BasketItemValidator());
        }
    }
}
