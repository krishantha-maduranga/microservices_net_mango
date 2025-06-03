using AutoMapper;
using Mango.Services.CouponAPI.DAL;
using Mango.Services.CouponAPI.DTOs;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private ResponseDto apiResponse;

        public CouponsController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            apiResponse = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var coupons = await _dbContext.Coupons.ToListAsync();
                apiResponse.Result = _mapper.Map<List<CouponDto>>(coupons);
                apiResponse.IsSuccess = true;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
                if(coupon != null)
                {
                    apiResponse.Result = _mapper.Map<CouponDto>(coupon);
                    apiResponse.IsSuccess = true;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = $"No coupon found with id, {id}";
                    return NotFound(apiResponse);
                }
                
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            try
            {
                var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower() == code.ToLower());
                if (coupon != null)
                {
                    apiResponse.Result = _mapper.Map<CouponDto>(coupon);
                    apiResponse.IsSuccess = true;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = $"No coupon found with code, {code}";
                    return NotFound(apiResponse);
                }

            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpPost]        
        public async Task<IActionResult> CreateAsync([FromBody] CouponDto couponDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var newCoupon = _mapper.Map<Coupon>(couponDto);
                    var result = await _dbContext.Coupons.AddAsync(newCoupon);
                    await _dbContext.SaveChangesAsync();
                    var createdCoupon = result.Entity;
                    return CreatedAtRoute("GetById", new {id = createdCoupon.CouponId},
                        createdCoupon);
                }
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CouponDto couponDto)
        {
            if (id != couponDto.CouponId)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = "Coupon ID mismatch.";
                return BadRequest(apiResponse);
            }

            try
            {
                var existingCoupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
                if (existingCoupon == null)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = $"No coupon found with id {id}.";
                    return NotFound(apiResponse);
                }

                // Map updated values from DTO to entity
                _mapper.Map(couponDto, existingCoupon);

                _dbContext.Coupons.Update(existingCoupon);
                await _dbContext.SaveChangesAsync();

                apiResponse.IsSuccess = true;
                apiResponse.Result = _mapper.Map<CouponDto>(existingCoupon);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingCoupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
                if (existingCoupon == null)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = $"No coupon found with id {id}.";
                    return NotFound(apiResponse);
                }

                _dbContext.Coupons.Remove(existingCoupon);
                await _dbContext.SaveChangesAsync();

                apiResponse.IsSuccess = true;
                apiResponse.Message = $"Coupon with id {id} was deleted successfully.";
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.Message = ex.Message;
                return StatusCode(500, apiResponse);
            }
        }


    }

}
