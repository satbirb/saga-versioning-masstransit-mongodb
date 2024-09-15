using MassTransit;
using OrderSaga.StateMachines;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MassTransit
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    // Saga configuration
    x.AddSagaStateMachine<OrderStateMachine, OrderState, OrderStateMachineDefinition>()
        //.InMemoryRepository(); // InMemory does not increment saga version
        .MongoDbRepository(r =>
        {
            r.Connection = "mongodb://localhost";
            r.DatabaseName = "OrderSaga";
        }); // Mongodb increments saga version always

    // Configure Transport
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
