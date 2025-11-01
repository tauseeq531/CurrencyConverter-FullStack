
using AutoMapper;
using CurrencyConverter.Api.Data;
using CurrencyConverter.Api.Repositories;
using CurrencyConverter.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// --- Database ---
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("SqlServer") 
             ?? "Server=(localdb)\\MSSQLLocalDB;Database=CurrencyConverterDb;Trusted_Connection=True;TrustServerCertificate=True;";
    options.UseSqlServer(cs);
});

// --- DI ---
builder.Services.AddScoped<IConversionRepository, EfConversionRepository>();
builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();

// --- MVC + Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
