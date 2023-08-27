using Microsoft.EntityFrameworkCore;
using PlatformManager.CommandService.AsyncDataServices;
using PlatformManager.CommandService.Data;
using PlatformManager.CommandService.EventProcessing;
using PlatformManager.CommandService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

builder.Services.AddScoped<ICommandRepo, CommandRepo>();

builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.PrepPopulation();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
