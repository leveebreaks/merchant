using System.Text.Json.Serialization;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants.UpdateMerchant;

public class UpdateMerchantRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Category Category { get; set; }
}