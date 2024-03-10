using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Data;
using MyPharmacy.Data.Repository;
using MyPharmacy.Data.Repository.Interfaces;
using MyPharmacy.Services;
using MyPharmacy.Services.Interfaces;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddMemoryCache(c =>
{
    c.TrackStatistics = true;
});

builder.Services.AddCors();

builder.Services.AddSwaggerDocument();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

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

builder.Services.AddScoped<IPharmacyService, PharmacyService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IPharmacistService, PharmacistService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IReportingService, ReportingService>();
builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IPharmacistRepository, PharmacistRepository>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
//builder.Services.AddScoped<IReportRepository, ReportRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    app.UseOpenApi();
    app.UseSwaggerUI(c =>
    {
        //c.DisplayRequestDuration();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPharmacy API V1");
        c.ConfigObject = new ConfigObject
        {
            DeepLinking = true,
            DefaultModelsExpandDepth = 5,
            DefaultModelExpandDepth = 5,
            DefaultModelRendering = Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model,
            DisplayOperationId = true,
            DisplayRequestDuration = true,
            DocExpansion = Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List,
            ShowExtensions = true
        };
    });

}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
