
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.DataModel;
using Domain.Interfaces;
using Domain.Models;
using Domain.IRepository;
using Infrastructure.Resolvers;
using Domain.Factory;
using Application.IServices;
using Application.Services;
using Application.DTO;
using MassTransit;
using InterfaceAdapters.Consumers;
using Application.IPublisher;
using InterfaceAdapters.Publisher;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var environment = builder.Environment.EnvironmentName;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Services
builder.Services.AddTransient<ITechnologyService, TechnologyService>();
builder.Services.AddTransient<IMessagePublisher, MassTransitPublisher>();


//Repositories
builder.Services.AddTransient<ITechnologyRepositoryEF, TechnologyRepositoryEF>();


//Factories
builder.Services.AddTransient<ITechnologyFactory, TechnologyFactory>();


//Mappers
builder.Services.AddTransient<TechnologyDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<Technology, TechnologyDTO>();
});
// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TechnologyCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
   {
       cfg.Host("rabbitmq://localhost");
       var instance = InstanceInfo.InstanceId;
       cfg.ReceiveEndpoint($"technologyMicroService-cmd-{instance}", e =>
       {
           e.ConfigureConsumer<TechnologyCreatedConsumer>(context);
       });
   });
});
//sender:


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();



app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }