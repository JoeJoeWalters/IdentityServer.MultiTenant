using Duende.IdentityServer.EntityFramework.DbContexts;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.MultiTenant.Data
{
    // https://www.finbuckle.com/MultiTenant/Docs/v6.9.1/EFCore
    // https://github.com/AndrewTriesToCode/MultiTenantIdentityServer4/blob/main/src/IdentityServerAspNetIdentity/Data/MultiTenantConfigurationDbContext.cs
    public class MultiConfigurationDbContext : ConfigurationDbContext<MultiConfigurationDbContext>
    {
        private ITenantInfo _tenantInfo;

        public MultiConfigurationDbContext(DbContextOptions<MultiConfigurationDbContext> options) : base(options)
        {
        }

        public MultiConfigurationDbContext(ITenantInfo tenantInfo, DbContextOptions<MultiConfigurationDbContext> options) : base(options)
        {
            _tenantInfo = tenantInfo;   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_tenantInfo.ConnectionString ?? throw new InvalidOperationException());
            //optionsBuilder.UseSqlite(TenantInfo.ConnectionString ?? throw new InvalidOperationException());
            base.OnConfiguring(optionsBuilder);
        }
    }
}
