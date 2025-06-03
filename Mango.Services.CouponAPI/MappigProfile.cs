using AutoMapper;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI
{
    public class MappigProfile : Profile
    {
        public MappigProfile()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
