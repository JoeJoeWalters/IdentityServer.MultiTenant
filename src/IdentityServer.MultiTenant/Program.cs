using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<ApplicationDbContext>();
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Finbuckle multi-tenant handling
builder.Services.AddMultiTenant<TenantInfo>()
                .WithHostStrategy()
                .WithConfigurationStore();

var app = builder.Build();

// Apply migrations if needed
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
