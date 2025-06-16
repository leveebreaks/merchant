using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UDC.MerchantApi.Domain;
using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants.GetMerchants;

public static class GetMerchantsEndpoint
{
    public static RouteGroupBuilder MapGetMerchants(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapGet("/", async (string name, string category, AppDbContext db, IMapper mapper) =>
        {
            IQueryable<Merchant> query = db.Merchants;
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(m => m.Name == name);
            } 
            else if (!string.IsNullOrWhiteSpace(category))
            {
                Enum.TryParse(category, true, out Category categoryEnum);
                query = query.Where(m => m.Category == categoryEnum);
            }
            var merchants = await query.Select(x => mapper.Map<MerchantDto>(x)).ToListAsync();
            return Results.Ok(merchants);
        });
        
        return routeGroup;
    }
}