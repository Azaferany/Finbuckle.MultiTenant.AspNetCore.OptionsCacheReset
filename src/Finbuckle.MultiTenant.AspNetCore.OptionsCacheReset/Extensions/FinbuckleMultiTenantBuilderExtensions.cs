using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Finbuckle.MultiTenant.AspNetCore.OptionsCacheReset.Extensions
{
    public static class FinbuckleMultiTenantBuilderExtensions
    {
        public static FinbuckleMultiTenantBuilder<TVersionTenantInfo>
            WithPerTenantManagedCacheOptions<TVersionTenantInfo, TOptions>(
            FinbuckleMultiTenantBuilder<TVersionTenantInfo> builder,
            Action<TOptions, TVersionTenantInfo> tenantConfigureOptions)
        where TOptions : class, new()
        where TVersionTenantInfo : class, IVersionTenantInfo, new()

    {
        builder.WithPerTenantOptions(tenantConfigureOptions);
        builder.Services.TryAddSingleton<TenantVersionStore>();
        builder.Services.TryAddSingleton(new MultiTenantOptionMark(typeof(TOptions)));
        return builder;
        }
    }
}