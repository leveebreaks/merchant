using System.Text.Json;
using System.Text.Json.Serialization;
using UDC.MerchantApi.Common;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants.GetMerchants;

public class GetMerchantsRequest
{
    public string? Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<Category>))]
    public Category? Category { get; set; }
}