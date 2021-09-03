using Microsoft.AspNetCore.Builder;

namespace Finbuckle.MultiTenant.AspNetCore.OptionsCacheReset.Extensions
{
    public static class FinbuckleMultiTenantApplicationBuilderExtensions
    {
        
        public static IApplicationBuilder UseMultiTenantOptionsResetManager<TVersionTenantInfo>(this IApplicationBuilder builder)
            where TVersionTenantInfo : class, IVersionTenantInfo, new()
            => builder.UseMiddleware<MultiTenantOptionManagerMiddleware<TVersionTenantInfo>>();
    }
}