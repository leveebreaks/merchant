using FluentValidation;

namespace UDC.MerchantApi.Features.Merchants.CreateMerchant;

public class CreateMerchantRequestValidator : AbstractValidator<CreateMerchantRequest>
{
    public CreateMerchantRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Category).IsInEnum().WithMessage("Category must be a valid enum.");
    }
}