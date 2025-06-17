using AutoMapper;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants;

public class MerchantMappingProfile : Profile
{
    public MerchantMappingProfile()
    {
        CreateMap<MerchantDto, Merchant>();
        CreateMap<Merchant, MerchantDto>();
    }
}