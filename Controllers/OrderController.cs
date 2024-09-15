using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OrderSaga.Contracts;

namespace OrderSaga.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IRequestClient<SubmitOrder> _submitOrderRequestClient;
    private readonly IRequestClient<CheckOrderStatus> _checkOrderStatusRequestClient;

    public OrderController(IRequestClient<SubmitOrder> submitOrderRequestClient, IRequestClient<CheckOrderStatus> checkOrderStatusRequestClient)
    {
        _submitOrderRequestClient = submitOrderRequestClient;
        _checkOrderStatusRequestClient = checkOrderStatusRequestClient;
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrder order)
    {
        var response = await _submitOrderRequestClient.GetResponse<OrderStatusResponse>(order);
        return Ok(response.Message);
    }

    [HttpGet("status/{orderId}")]
    public async Task<IActionResult> CheckOrderStatus(Guid orderId)
    {
        var response = await _checkOrderStatusRequestClient.GetResponse<OrderStatusResponse>(new CheckOrderStatus { OrderId = orderId });
        return Ok(response.Message);
    }
}
