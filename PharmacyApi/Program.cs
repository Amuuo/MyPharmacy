using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Services;
using PharmacyApi.Services.Interfaces;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<IPharmacyDbContext, PharmacyDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions => {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, 
            maxRetryDelay: TimeSpan.FromSeconds(30), 
            errorNumbersToAdd: null);
    });
});

builder.Services.AddTransient<IPharmacyService, PharmacyService>();
builder.Services.AddTransient<IDeliveryService, DeliveryService>();
builder.Services.AddTransient<IPharmacistService, PharmacistService>();
builder.Services.AddTransient<IWarehouseService, WarehouseService>();
builder.Services.AddTransient<IReportService, ReportService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
