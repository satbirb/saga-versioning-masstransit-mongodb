using System;
namespace OrderSaga.Contracts;

public class SubmitOrder
{
    public Guid OrderId { get; set; }
    public string OrderDetails { get; set; }
}

public class CheckOrderStatus
{
    public Guid OrderId { get; set; }
    public int CurrentVersion { get; set; }
}

public class OrderStatusResponse
{
    public Guid OrderId { get; set; }
    public int CurrentVersion { get; set; }
}
