namespace UDC.MerchantApi.Domain;

public class Merchant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
}