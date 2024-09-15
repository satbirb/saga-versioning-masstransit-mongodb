using MassTransit;
using MassTransit.Saga;
using OrderSaga.Contracts;

namespace OrderSaga.StateMachines;
public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => OrderStatusRequested, x => x.CorrelateById(m => m.Message.OrderId));

        Initially(
            When(OrderSubmitted)
                .ThenAsync(async context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.OrderDetails = context.Message.OrderDetails;

                    await context.RespondAsync<OrderStatusResponse>(new
                    {
                        context.Message.OrderId,
                        CurrentVersion = context.Saga.Version
                    });
                })
                .TransitionTo(Submitted));

        During(Submitted,
            When(OrderStatusRequested)
                // Respond to the status request with no changes to saga state
                .RespondAsync(context => context.Init<OrderStatusResponse>(new
                {
                    OrderId = context.Saga.OrderId,
                    CurrentVersion = context.Saga.Version
                })));
    }

    public State Submitted { get; private set; }

    public Event<SubmitOrder> OrderSubmitted { get; private set; }
    public Event<CheckOrderStatus> OrderStatusRequested { get; private set; }
}
