using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.MultiTenant.Data
{
    // https://www.finbuckle.com/MultiTenant/Docs/v6.9.1/EFCore
    // https://github.com/AndrewTriesToCode/MultiTenantIdentityServer4/blob/main/src/IdentityServerAspNetIdentity/Data/MultiTenantConfigurationDbContext.cs
    public class ApplicationDbContext : MultiTenantIdentityDbContext
    {
        public ApplicationDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public ApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options) :
            base(tenantInfo, options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<ApplicationUser>().IsMultiTenant();
        }
    }
}
