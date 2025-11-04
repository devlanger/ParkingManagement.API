using CarAssignment.Application.CQRS.Command.AllocateVehicleCommand;
using CarAssignment.Application.CQRS.Extensions;
using CarAssignment.Application.Extensions;
using CarAssignment.Core.Configuration;
using CarAssignment.ExceptionHandlers;
using CarAssignment.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplicationCqrsServices();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddApplicationServices();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AllocateVehicleCommandHandler).Assembly);
});

builder.Services.Configure<ParkingConfiguration>(
    builder.Configuration.GetSection("ParkingConfiguration"));

var app = builder.Build();

await app.Services.AddMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler((x) => {});

app.MapControllers().WithOpenApi();
app.Run();
