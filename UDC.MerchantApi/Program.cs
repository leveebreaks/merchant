using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UDC.MerchantApi.Features.Merchants.CreateMerchant;
using UDC.MerchantApi.Features.Merchants.DeleteMerchant;
using UDC.MerchantApi.Features.Merchants.GetMerchants;
using UDC.MerchantApi.Features.Merchants.UpdateMerchant;
using UDC.MerchantApi.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var merchantsGroup = app.MapGroup("/api/merchants");
merchantsGroup
    .MapGetMerchants()
    .MapCreateMerchant()
    .MapUpdateMerchant()
    .MapDeleteMerchant();
    
app.Run();