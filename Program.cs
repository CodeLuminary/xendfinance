using MassTransit;
using Microsoft.EntityFrameworkCore;
using Xend_Finance.Entities;
using Xend_Finance.Interface;
using Xend_Finance.Interface.Repository;
using Xend_Finance.Repository;
using Xend_Finance.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

//Add Dependency injection here
builder.Services.AddSingleton<ICryptoApiService, CryptoApiService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IEventBus, EventBus>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TransactionService>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(factoryConfigurator =>
    {
        //cfg.UseHealthCheck()
        factoryConfigurator.Host(new Uri(builder.Configuration["MessageQueue:Url"].ToString()), hostConfigurator =>
        {
            hostConfigurator.Username(builder.Configuration["MessageQueue:Username"].ToString());
            hostConfigurator.Password(builder.Configuration["MessageQueue:Password"].ToString());
        });
        factoryConfigurator.ReceiveEndpoint(builder.Configuration["MessageQueue:QueueName"].ToString(), endpointConfigurator =>
        {
            endpointConfigurator.PrefetchCount = 16;
            endpointConfigurator.UseMessageRetry(retryConfigurator => retryConfigurator.Interval(5, 100));
            endpointConfigurator.ConfigureConsumer<TransactionService>(provider);
        });
    }));
});

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
