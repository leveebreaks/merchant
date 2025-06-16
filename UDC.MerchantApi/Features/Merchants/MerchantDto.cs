using System.Text.Json.Serialization;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants;

public class MerchantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Category Category { get; set; }
}