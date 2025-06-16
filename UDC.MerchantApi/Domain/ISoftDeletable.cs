namespace UDC.MerchantApi.Domain;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}