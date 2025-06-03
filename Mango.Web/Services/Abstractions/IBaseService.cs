using Mango.Web.DTOs;

namespace Mango.Web.Services.Abstractions
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
