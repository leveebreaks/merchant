using AutoMapper;
using FluentValidation;
using UDC.MerchantApi.Common;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants.CreateMerchant;

public static class CreateMerchantEndpoint
{
    public static RouteGroupBuilder MapCreateMerchant(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapPost("/", async (
            CreateMerchantRequest request, 
            IMerchantRepository repository, 
            IMapper mapper, 
            IValidator<CreateMerchantRequest> validator) =>
        {
            var validationResult = await request.ValidateRequest(validator);
            if (validationResult is not null) return validationResult;
            
            var merchant = new Merchant
            {
                Name = request.Name,
                Email = request.Email,
                Category = request.Category,
            };

            await repository.AddAsync(merchant);
            await repository.SaveChangesAsync();

            return Results.Created($"/api/merchants/{merchant.Id}", mapper.Map<MerchantDto>(merchant));
        });

        return routeGroup;
    }
}