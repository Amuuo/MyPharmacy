using Microsoft.EntityFrameworkCore;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Data;
using MyPharmacy.Services;
using MyPharmacy.Services.Interfaces;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<IPharmacyDbContext, PharmacyDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions => {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, 
            maxRetryDelay: TimeSpan.FromSeconds(30), 
            errorNumbersToAdd: null);
        //sqlOptions.MigrationsAssembly("MyPharmacy.Api");
    });
});

builder.Services.AddTransient<IPharmacyService, PharmacyService>();
builder.Services.AddTransient<IDeliveryService, DeliveryService>();
builder.Services.AddTransient<IPharmacistService, PharmacistService>();
builder.Services.AddTransient<IWarehouseService, WarehouseService>();
builder.Services.AddTransient<IReportService, ReportingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
