using AutoMapper;
using UDC.MerchantApi.Domain;

namespace UDC.MerchantApi.Features.Merchants;

public class MerchantProfile : Profile
{
    public MerchantProfile()
    {
        CreateMap<MerchantDto, Merchant>();
        CreateMap<Merchant, MerchantDto>();
    }
}