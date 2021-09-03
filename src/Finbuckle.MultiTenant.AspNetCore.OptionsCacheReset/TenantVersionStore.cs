using System;
using System.Collections.Concurrent;

namespace Finbuckle.MultiTenant.AspNetCore.OptionsCacheReset
{
    public class TenantVersionStore
    {
        private readonly ConcurrentDictionary<string, int> _tenantVersionDictionary =
            new(StringComparer.Ordinal);

        /// <summary>
        ///     Gets tenant version or set it
        /// </summary>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="version">Last tenant version</param>
        /// <returns>The tenant version.</returns>
        public int GetVersion(string tenantId)
        {
            return _tenantVersionDictionary.TryGetValue(tenantId, out var version) ? 0 : version;
        }

        public int SetVersion(string tenantId, int newTenantVersion)
        {
            return _tenantVersionDictionary.AddOrUpdate(tenantId, newTenantVersion, (_, _) => newTenantVersion);
        }
    }
}