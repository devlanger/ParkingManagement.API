using System.Reflection;
using CarAssignment.Application.Configuration;
using CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;
using CarAssignment.Application.Extensions;
using CarAssignment.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AllocateVehicleCommand).GetTypeInfo().Assembly));

builder.Services.Configure<ParkingConfiguration>(
    builder.Configuration.GetSection("ParkingConfiguration"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers().WithOpenApi();
app.Run();
