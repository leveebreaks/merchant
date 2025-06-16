using AutoMapper;
using UDC.MerchantApi.Domain;
using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants.DeleteMerchant;

public static class DeleteMerchantEndpoint
{
    public static RouteGroupBuilder MapDeleteMerchant(this RouteGroupBuilder routeGroup)
    {
        routeGroup.MapDelete("/{id:int}", async (int id, AppDbContext db, IMapper mapper) =>
        {
            if (await db.Merchants.FindAsync(id) is { } merchant)
            {
                // extract this to a generic repo
                if (merchant is ISoftDeletable)
                {
                    merchant.IsDeleted = true;
                }
                else
                {
                    db.Merchants.Remove(merchant);
                }
                await db.SaveChangesAsync();
                return Results.Ok(mapper.Map<MerchantDto>(merchant));
            }
            
            return Results.NotFound();
        });
        
        return routeGroup;
    }
}