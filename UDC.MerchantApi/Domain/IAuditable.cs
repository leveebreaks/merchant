namespace UDC.MerchantApi.Domain;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
}