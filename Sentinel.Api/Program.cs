using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;
using Sentinel.Api.Repositories;
using Sentinel.Api.Repositories.Interfaces;
using Sentinel.Api.Services;
using Sentinel.Api.Services.Interfaces;
using Sentinel.Api.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IMonitorRepository, MonitorRepository>();
builder.Services.AddScoped<IMonitorService, MonitorService>();
builder.Services.AddScoped<ICheckLogRepository, CheckLogRepository>();

builder.Services.AddHostedService<UptimeCheckerWorker>();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
