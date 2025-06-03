using System.Reflection;

namespace Mango.Services.CouponAPI
{
    public static class CouponApiServiceRegistration
    {
        public static IServiceCollection AddCouponApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
