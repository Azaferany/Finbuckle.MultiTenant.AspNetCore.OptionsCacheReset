#This is helper For Auto Invalidation Of IOptions<> and IOptionsMonitorOption<> cache during request

## how Work?
this Helper Track Tenant Version during request and if Version of Option in the cache is not equal to tenant resolve by `app.UseMultiTenant()` will clear cached options of the current Tenant  

## Usage 
1. use `IVersionTenantInfo` instead of `ITenantInfo`
2. use `WithPerTenantManagedCacheOptions` instead of `WithPerTenantOptions`
3. add `app.UseMultiTenantOptionsResetManager<YourTenantInfo>()` after `app.UseMultiTenant()`

now change value of `TenantInfo.Version` when some tenant options need to change
