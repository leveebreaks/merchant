using UDC.MerchantApi.Infrastructure.Persistance;

namespace UDC.MerchantApi.Features.Merchants.UpdateMerchant;

public static class UpdateMerchantEndpoint
{
    public static RouteGroupBuilder MapUpdateMerchant(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:int}", async (int id, UpdateMerchantRequest request, AppDbContext db) =>
        {
            var merchant = await db.Merchants.FindAsync(id);
            if (merchant == null)
                return Results.NotFound();

            merchant.Name = request.Name;
            merchant.Email = request.Email;
            merchant.Category = request.Category;

            await db.SaveChangesAsync();
            return Results.Ok(merchant);
        });

        return group;
    }
}