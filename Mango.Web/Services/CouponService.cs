using Mango.Web.DTOs;
using Mango.Web.Services.Abstractions;

namespace Mango.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto?> CreateAsync(CouponDto coupon)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateAsync(CouponDto coupon)
        {
            throw new NotImplementedException();
        }
    }
}
