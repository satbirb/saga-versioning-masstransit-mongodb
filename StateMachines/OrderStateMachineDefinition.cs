using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace OrderSaga.StateMachines;
public class OrderStateMachineDefinition : SagaDefinition<OrderState>
{
    /// <summary>
    ///  Handles configuration for the Order Saga
    /// </summary>
    /// <param name="endpointConfigurator"></param>
    /// <param name="sagaConfigurator"></param>
    /// <param name="context"></param>
    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<OrderState> sagaConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}
