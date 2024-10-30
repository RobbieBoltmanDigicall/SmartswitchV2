using SmartSwitchV2.DataLayer.HTTPDefinitions;
using ClaimsService.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ClaimsService.Models.Repositories;
using SmartSwitchV2.DataLayer.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//TODO: Investigate this ReferenceHandler.Preserve to avoid cycle references if it is the best approach.
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SmartSwitchDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});

builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IResponseRepository, ResponseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
