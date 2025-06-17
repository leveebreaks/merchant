using FluentValidation;

namespace UDC.MerchantApi.Features.Merchants.UpdateMerchant;

public class UpdateMerchantRequestValidator : AbstractValidator<UpdateMerchantRequest>
{
    public UpdateMerchantRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
    }
}