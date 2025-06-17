using FluentValidation;

namespace UDC.MerchantApi.Common;

public static class ValidationExtensions
{
    public static async Task<IResult?> ValidateRequest<T>(
        this T request,
        IValidator<T> validator)
    {
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return Results.ValidationProblem(errors);
        }

        return null!;
    }
}