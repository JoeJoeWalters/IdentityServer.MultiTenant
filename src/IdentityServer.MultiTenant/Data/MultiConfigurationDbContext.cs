using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.MultiTenant.Data
{
    // https://www.finbuckle.com/MultiTenant/Docs/v6.9.1/EFCore
    // https://github.com/AndrewTriesToCode/MultiTenantIdentityServer4/blob/main/src/IdentityServerAspNetIdentity/Data/MultiTenantConfigurationDbContext.cs
    public class MultiConfigurationDbContext : ConfigurationDbContext<MultiConfigurationDbContext>, IMultiTenantDbContext
    {
        public MultiConfigurationDbContext(DbContextOptions<MultiConfigurationDbContext> options) : base(options)
        {
        }

        public MultiConfigurationDbContext(ITenantInfo tenantInfo, DbContextOptions<MultiConfigurationDbContext> options) : base(options)
        {
            TenantInfo = tenantInfo;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>().IsMultiTenant();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.EnforceMultiTenant();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            this.EnforceMultiTenant();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public ITenantInfo TenantInfo { get; }

        public TenantMismatchMode TenantMismatchMode { get; } = TenantMismatchMode.Throw;

        public TenantNotSetMode TenantNotSetMode { get; } = TenantNotSetMode.Throw;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(TenantInfo.ConnectionString ?? throw new InvalidOperationException());
            //optionsBuilder.UseSqlite(TenantInfo.ConnectionString ?? throw new InvalidOperationException());
            base.OnConfiguring(optionsBuilder);
        }
    }
}
