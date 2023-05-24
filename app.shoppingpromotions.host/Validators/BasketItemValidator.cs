using app.shoppingpromotions.domain;
using FluentValidation;

namespace app.shoppingpromotions.host.Validators
{
    public class BasketItemValidator : AbstractValidator<BasketItem>
    {
        public BasketItemValidator()
        {
            RuleFor(dto => dto.ProductId).NotEmpty().WithMessage("Product ID is required.");
            RuleFor(dto => dto.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0.");
            RuleFor(dto => dto.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }

}
