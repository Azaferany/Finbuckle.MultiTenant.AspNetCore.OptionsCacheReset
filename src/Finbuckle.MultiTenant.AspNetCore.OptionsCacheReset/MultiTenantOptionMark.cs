using System;
using Microsoft.Extensions.Options;

namespace Finbuckle.MultiTenant.AspNetCore.OptionsCacheReset
{
    public class MultiTenantOptionMark
    {
        public MultiTenantOptionMark(Type optionType)
        {
            OptionsMonitorCacheOptionType = typeof(IOptionsMonitorCache<>).MakeGenericType(optionType);
            OptionsMonitorOptionType = typeof(IOptionsMonitor<>).MakeGenericType(optionType);
            OptionsCacheOptionType = typeof(IOptions<>).MakeGenericType(optionType);
        }

        public Type OptionsMonitorCacheOptionType { get; }
        public Type OptionsMonitorOptionType { get; }
        public Type OptionsCacheOptionType { get; }
    }
}