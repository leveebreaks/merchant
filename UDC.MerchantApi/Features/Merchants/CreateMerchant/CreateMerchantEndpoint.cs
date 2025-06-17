using AutoMapper;
using FluentValidation;
using UDC.MerchantApi.Common;
using UDC.MerchantApi.Domain;
using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants.CreateMerchant;

public static class CreateMerchantEndpoint
{
    public static RouteGroupBuilder MapCreateMerchant(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapPost("/", async (
            CreateMerchantRequest request, 
            AppDbContext db, 
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

            db.Merchants.Add(merchant);
            await db.SaveChangesAsync();

            return Results.Created($"/api/merchants/{merchant.Id}", mapper.Map<MerchantDto>(merchant));
        });

        return routeGroup;
    }
}