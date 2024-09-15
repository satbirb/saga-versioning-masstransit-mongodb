using System;
using MassTransit;
using MassTransit.Saga;

namespace OrderSaga.StateMachines;
public class OrderState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid OrderId { get; set; }
    public string OrderDetails { get; set; }

    /// <inheritdoc/>
    public int Version { get; set; }
}