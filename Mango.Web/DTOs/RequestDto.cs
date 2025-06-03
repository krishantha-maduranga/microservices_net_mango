using Mango.Web.Enums;
using static Mango.Web.Enums.Enums;

namespace Mango.Web.DTOs
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public required string Url { get; set; }
        public dynamic? Data { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}
