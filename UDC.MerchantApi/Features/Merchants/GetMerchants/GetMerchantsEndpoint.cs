using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants.GetMerchants;

public static class GetMerchantsEndpoint
{
    public static RouteGroupBuilder MapGetMerchants(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapGet("/", async (string category, string name, AppDbContext db, IMapper mapper) =>
        {
            var merchants = await db.Merchants.Select(x => mapper.Map<MerchantDto>(x)).ToListAsync();
            return Results.Ok(merchants);
        });
        
        return routeGroup;
    }
}