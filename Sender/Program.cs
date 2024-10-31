using Core;
using MassTransit;
using MassTransit.Transports.Fabric;
using ExchangeType = RabbitMQ.Client.ExchangeType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Task.Delay(1000);
builder.Services.AddMassTransit(x =>
{
    //x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((bus, rabbit) =>
    {
        rabbit.Host("rabbit", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        //rabbit.ConfigureEndpoints(bus);
        //rabbit.MessageD<MessageD>(e => e.SetEntityName("test"));
        //rabbit.Message<MessageD>(e => {});
        rabbit.Publish<MessageD>();
        rabbit.ConfigureEndpoints(bus);
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
