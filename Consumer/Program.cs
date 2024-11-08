using MassTransit;
using Microsoft.AspNetCore.WebSockets;
using RabbitMQ.Client;

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
    x.AddConsumer<Consumer.MessageDConsumer>();
    x.UsingRabbitMq((bus, rabbit) =>
    {

        //var assembly = typeof(Program).Assembly;
        //x.AddConsumers(assembly);

        rabbit.Host("rabbit", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        rabbit.ConfigureEndpoints(bus);


        //rabbit.ReceiveEndpoint("qqqq-q", re =>
        //{
        //    re.ConfigureConsumeTopology = false;

        //    re.MessageDConsumer<MessageDConsumer.MessageDConsumer>();

        //    re.Bind("test", e =>
        //    {
        //        e.RoutingKey = "";
        //        e.ExchangeType = ExchangeType.Direct;
        //    });

        //});
    });

});

var app = builder.Build();

await OtherOperations();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWebSockets();

app.Run();

async Task OtherOperations()
{
    Consumer.MessageDConsumer.QueueMessageHandler += (message) =>
    {
        Console.WriteLine(message.Message);
        return Task.CompletedTask;
    };
}
