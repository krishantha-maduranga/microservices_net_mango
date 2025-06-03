using Mango.Web.DTOs;

namespace Mango.Web.Services.Abstractions
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllAsync();
        Task<ResponseDto?> GetByIdAsync(int id);
        Task<ResponseDto?> GetByCodeAsync(string code);
        Task<ResponseDto?> CreateAsync(CouponDto coupon);
        Task<ResponseDto?> UpdateAsync(CouponDto coupon);
        Task<ResponseDto?> DeleteAsync(int id);
    }
}
