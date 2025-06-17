using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants.GetMerchants;

public static class GetMerchantsEndpoint
{
    public static RouteGroupBuilder MapGetMerchants(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapGet("/", async (
            string? name,
            string? category, 
            IMerchantRepository repository, 
            IMapper mapper) =>
        {
            Category? categoryEnum = null;
            if (!string.IsNullOrWhiteSpace(category) && Enum.TryParse(category, out Category parsedCategory))
            {
                categoryEnum = parsedCategory;
            }
            
            var merchants = 
                await repository
                    .GetFilteredAsync(categoryEnum, name);
            return Results.Ok(merchants.Select(mapper.Map<MerchantDto>));
        });
        
        return routeGroup;
    }
}