using Finbuckle.MultiTenant;
using IdentityServer.MultiTenant.Data;
using IdentityServer.MultiTenant.OpenApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>();
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.OperationFilter<AddRequiredHeaderParameter>());

// Add Finbuckle multi-tenant handling
builder.Services.AddMultiTenant<TenantInfo>()
                .WithHeaderStrategy("TenantKey")
                .WithConfigurationStore();

var app = builder.Build();

// Apply migrations if needed (Doesn't work with InMemory obvs)
/*
var store = app.Services.GetRequiredService<IMultiTenantStore<TenantInfo>>();
foreach (var tenant in await store.GetAllAsync())
{
    await using var db = new ApplicationDbContext(tenant);
    await db.Database.MigrateAsync();
}
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Must come before UseEndpoints (or equiv)
app.UseMultiTenant();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
