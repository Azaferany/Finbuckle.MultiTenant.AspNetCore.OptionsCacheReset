using System;
using System.Threading.Tasks;
using Finbuckle.MultiTenant.AspNetCore.OptionsCacheRest;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Finbuckle.MultiTenant.AspNetCore.OptionsCacheReset
{
    public class MultiTenantOptionManagerMiddleware<TVersionTenantInfo> 
        where TVersionTenantInfo : class, IVersionTenantInfo, new()
    {
        private readonly RequestDelegate _next;
        private readonly TenantVersionStore _tenantVersionStore;

        public MultiTenantOptionManagerMiddleware(
            RequestDelegate next,
            TenantVersionStore tenantVersionStore
        )
        {
            this._next = next;
            this._tenantVersionStore = tenantVersionStore;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessor = context.RequestServices.GetRequiredService<IMultiTenantContextAccessor<TVersionTenantInfo>>();

            if (accessor.MultiTenantContext == null)
            {
                throw new InvalidOperationException(
                    "Use MultiTenantOptionManagerMiddleware After MultiTenantMiddleware");
            }

            var tenantInfo = accessor.MultiTenantContext.TenantInfo;
            var version = _tenantVersionStore.GetVersion(tenantInfo.Id);
            
            if (tenantInfo.Version != version)
                foreach (var tenantOptionMark in context.RequestServices.GetServices<MultiTenantOptionMark>())
                    (context.RequestServices.GetRequiredService(tenantOptionMark.OptionsMonitorCacheOptionType) as IOptionsMonitorCache<object>)?.Clear();
            
            _tenantVersionStore.SetVersion(tenantInfo.Id, tenantInfo.Version);
            
            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}