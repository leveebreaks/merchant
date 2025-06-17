using AutoMapper;
using FluentValidation;
using UDC.MerchantApi.Common;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants.UpdateMerchant;

public static class UpdateMerchantEndpoint
{
    public static RouteGroupBuilder MapUpdateMerchant(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:int}", async (
            int id, 
            UpdateMerchantRequest request, 
            IMerchantRepository repository, 
            IValidator<UpdateMerchantRequest> validator,
            IMapper mapper) =>
        {
            var validationResult = await request.ValidateRequest(validator);
            if (validationResult is not null) return validationResult;
            
            var merchant = await repository.GetByIdAsync(id);
            if (merchant == null)
                return Results.NotFound();

            merchant.Name = request.Name;
            merchant.Email = request.Email;
            merchant.Category = request.Category;

            await repository.SaveChangesAsync();
            return Results.Ok(mapper.Map<MerchantDto>(merchant));
        });

        return group;
    }
}