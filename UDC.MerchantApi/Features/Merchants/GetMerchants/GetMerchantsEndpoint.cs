using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants.GetMerchants;

public static class GetMerchantsEndpoint
{
    public static RouteGroupBuilder MapGetMerchants(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapGet("/", async (string category, string name, AppDbContext dbContext) =>
        {
            var merchants = await dbContext.Merchants.ToListAsync();
            return Results.Ok(merchants);
        });
        
        return routeGroup;
    }
}