using System.Text.Json.Serialization;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants.CreateMerchant;

public class CreateMerchantRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonConverter(typeof(JsonStringEnumConverter<Category>))]
    public Category Category { get; set; }
}